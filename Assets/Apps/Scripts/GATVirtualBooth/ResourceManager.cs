using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace GATVirtualBooth
{
    public static class ResourceManager
    {
        //addressables ops
        private static Dictionary<string, AsyncOperationHandle> instantiatedUniqueObjects = new Dictionary<string, AsyncOperationHandle>();

        //addressables initialization
        private static List<string> updatableCatalog = new List<string>();
        private static List<IResourceLocator> updatedResourceLocator = new List<IResourceLocator>();
        private static List<string> primaryKeys = new List<string>();

        //addressables initialization action
        private static event Action<float> DownloadProgress;
        private static event Action DownloadAssetFailed;
        private static event Action DownloadCompleted;

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
            var downloadHandle = Addressables.DownloadDependenciesAsync(primaryKeys, Addressables.MergeMode.Union, false);

            while (!downloadHandle.IsDone)
            {
                DownloadProgress?.Invoke(downloadHandle.GetDownloadStatus().Percent);
                await Task.Yield();
            }

            if (downloadHandle.Status != AsyncOperationStatus.Succeeded)
            {
                DownloadAssetFailed?.Invoke();
                return;
            }

            DownloadCompleted?.Invoke();
            Release(downloadHandle);
        }

        public static async Task InstantiateObject(string path)
        {
            var objectHandle = Addressables.InstantiateAsync(path);
            await objectHandle.Task;

            if (objectHandle.Status == AsyncOperationStatus.Succeeded)
            {
                instantiatedUniqueObjects.Add(path, objectHandle);
            }
            else
            {
                Debug.Log($"Fail to instantiate object: {path}");
            }
        }
    }
}
