using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GATVirtualBooth.AssetVerification
{
    public class AssetVerification : MonoBehaviour
    {
        [SerializeField] private PopUpConfirmation popUpConfirmation;
        [SerializeField] private PopUpNotice popUpNotice;

        public event Action InitializingAddressables;
        public event Action CheckingCatalog;
        public event Action OnConnectionAvailable;
        public event Action OnConnectionUnavailable;
        public event Action OnDownloadNeeded;

        private async void OnStart()
        {
            await ConnectionCheck();
            await BundleCheck();

            long downloadSize = await ResourceManager.GetDownloadSize();
            
            if (downloadSize > 0)
            {
                OnDownloadNeeded?.Invoke();

                int result = await popUpConfirmation.Show($"Need download {FileSize.ByteToMB(downloadSize)} MB\nProceed to download?", "No", "Yes");
                if (result == 1)
                {
                    await ResourceManager.UpdateBundle();
                }
                else if (result == 0)
                {
                    //exit application
#if UNITY_EDITOR
                    EditorApplication.isPlaying = false;
#endif
                    Application.Quit();
                }
            }
            else
            {
                GoToMainMenu();
            }
        }

        private async Task ConnectionCheck()
        {
            bool isNetworkAvailable = false;

            do
            {
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    int result = await popUpConfirmation.Show($"Network unavailable.\nCheck your connection and try again.", "Exit", "Retry");
                    if (result == 0)
                    {
                        //exit application
#if UNITY_EDITOR
                        EditorApplication.isPlaying = false;
#endif
                        Application.Quit();
                    }
                    else
                    {
                        //open setting or just skip

                    }
                }
                else
                {
                    isNetworkAvailable = true;
                }

                await Task.Delay(1000);
            } while (!isNetworkAvailable);
        }

        private async Task BundleCheck()
        {
            bool canConnectToHost = true;

            InitializingAddressables.Invoke();
            await ResourceManager.InitializeAddressables();

            CheckingCatalog?.Invoke();
            canConnectToHost = await ResourceManager.CatalogCheck();
            await ResourceManager.CatalogUpdate();

            if (canConnectToHost)
            {
                OnConnectionAvailable?.Invoke();
                Debug.Log($"Can connect to host.");
            }
            else
            {
                OnConnectionUnavailable?.Invoke();
                Debug.Log($"Cannot connect to host!");
            }
        }
        
        private async void GoToMainMenu()
        {
            await Task.Delay(1000);
            //load next scene
            await ResourceManager.LoadScene("Scenes/Main Menu.unity", LoadSceneMode.Single);
        }

        private void Start()
        {
            OnStart();
        }

        private void OnEnable()
        {
            ResourceManager.UpdateBundleCompleted += GoToMainMenu;
        }

        private void OnDisable()
        {
            ResourceManager.UpdateBundleCompleted -= GoToMainMenu;
        }
    }
}
