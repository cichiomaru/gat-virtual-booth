using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace GATVirtualBooth
{
    public static class ResourceManager
    {
        //addressables ops
        private static Dictionary<string, AsyncOperationHandle> instantiatedUniqueObjects = new Dictionary<string, AsyncOperationHandle>();
        private static Dictionary<string, SceneInstance> loadedScenes = new Dictionary<string, SceneInstance>();

        //addressables initialization
        private static List<string> updatableCatalog = new List<string>();
        private static List<IResourceLocator> updatedResourceLocator = new List<IResourceLocator>();
        private static List<string> primaryKeys = new List<string>();

        //addressables initialization action
        public static event Action<float> UpdateBundleProgress;
        public static event Action UpdateBundleFailed;
        public static event Action UpdateBundleStarted;
        public static event Action UpdateBundleCompleted;

        public static event Action LoadSceneStarted;
        public static event Action<float> LoadSceneProgress;
        public static event Action LoadSceneFinished;

        public static async Task InitializeAddressables()
        {
            await Addressables.InitializeAsync(true).Task;
        }

        private static void Release(AsyncOperationHandle handle)
        {
            Addressables.Release(handle);
        }

        public static async Task<bool> CatalogCheck()
        {
            var catalogCheckHandle = Addressables.CheckForCatalogUpdates(false);
            await catalogCheckHandle.Task;

            if (catalogCheckHandle.Status != AsyncOperationStatus.Succeeded)
            {
                return false;
            }

            updatableCatalog.AddRange(catalogCheckHandle.Result);
            Release(catalogCheckHandle);
            return true;
        }

        public static async Task<bool> CatalogUpdate()
        {
            if (updatableCatalog.Count == 0)
            {
                return true;
            }

            var updateHandle = Addressables.UpdateCatalogs(updatableCatalog, false);
            await updateHandle.Task;

            if (updateHandle.Status != AsyncOperationStatus.Succeeded)
            {
                return false;
            }

            updatedResourceLocator.AddRange(updateHandle.Result);
            Release(updateHandle);
            return true;
        }

        private static void RetrievePrimaryKey()
        {
            List<string> keys = new List<string>();

            foreach (var locator in Addressables.ResourceLocators)
            {
                foreach (var key in locator.Keys)
                {
                    IList<IResourceLocation> resourceLocations;
                    locator.Locate(key, null, out resourceLocations);

                    if (resourceLocations == null)
                    {
                        continue;
                    }

                    foreach (var location in resourceLocations)
                    {
                        string primary = location.PrimaryKey;

                        if (keys.Contains(primary))
                        {
                            break;
                        }

                        keys.Add(primary);
                    }
                }
            }
            primaryKeys.AddRange(keys);
        }

        public static async Task<long> GetDownloadSize()
        {
            RetrievePrimaryKey();
            primaryKeys.ForEach((e) => Debug.Log($"key: {e}"));
            var downloadSizeHandle = Addressables.GetDownloadSizeAsync(primaryKeys);
            await downloadSizeHandle.Task;

            if (downloadSizeHandle.Status != AsyncOperationStatus.Succeeded)
            {
                return -1;
            }

            long sizeInBytes = downloadSizeHandle.Result;
            Release(downloadSizeHandle);

            return sizeInBytes;
        }

        public static async Task UpdateBundle()
        {
            UpdateBundleStarted?.Invoke();

            var downloadHandle = Addressables.DownloadDependenciesAsync(primaryKeys, Addressables.MergeMode.Union, false);

            while (!downloadHandle.IsDone)
            {
                UpdateBundleProgress?.Invoke(downloadHandle.GetDownloadStatus().Percent);
                await Task.Yield();
            }

            if (downloadHandle.Status != AsyncOperationStatus.Succeeded)
            {
                UpdateBundleFailed?.Invoke();
                return;
            }

            UpdateBundleCompleted?.Invoke();
            Release(downloadHandle);
        }

        public static async Task<GameObject> InstantiateObject(string path)
        {
            var objectHandle = Addressables.InstantiateAsync(path);
            await objectHandle.Task;

            if (objectHandle.Status == AsyncOperationStatus.Succeeded)
            {
                instantiatedUniqueObjects.Add(path, objectHandle);
                return objectHandle.Result;
            }
            else
            {
                Debug.Log($"Fail to instantiate object: {path}");
                return null;
            }
        }

        public static async Task LoadScene(string path, LoadSceneMode mode)
        {
            float loadSceneProgress = 0f;
            float activateSceneProgress = 0f;

            LoadSceneStarted?.Invoke();

            var loadSceneHandle = Addressables.LoadSceneAsync(path, mode, false);
            while(!loadSceneHandle.IsDone)
            {
                loadSceneProgress = loadSceneHandle.GetDownloadStatus().Percent;
                LoadSceneProgress?.Invoke(loadSceneProgress * 0.7f + activateSceneProgress * 0.3f);
                await Task.Delay(100);
            }

            var activateScene = loadSceneHandle.Result.ActivateAsync();
            while (!activateScene.isDone)
            {
                activateSceneProgress = activateScene.progress;
                LoadSceneProgress?.Invoke(loadSceneProgress * 0.7f + activateSceneProgress * 0.3f);
                await Task.Delay(100);
            }

            loadedScenes.Add(path, loadSceneHandle.Result);
            Release(loadSceneHandle);

            LoadSceneFinished?.Invoke();
        }

        public static async Task UnloadScene(string path)
        {
            var unloadSceneHandle = Addressables.UnloadSceneAsync(loadedScenes[path], true);
            await unloadSceneHandle.Task;

            loadedScenes.Remove(path);
        }
    }
}
