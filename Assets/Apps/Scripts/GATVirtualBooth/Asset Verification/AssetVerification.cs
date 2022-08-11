using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace GATVirtualBooth.AssetVerification
{
    public class AssetVerification : MonoBehaviour
    {
        public event Action InitializingAddressables;
        public event Action CheckingCatalog;
        public event Action OnConnectionAvailable;
        public event Action OnConnectionUnavailable;
        public event Action OnDownloadNeeded;

        private async void OnStart()
        {
            Caching.ClearCache();

            await BundleCheck();

            long downloadSize = await ResourceManager.GetDownloadSize();
            
            if (downloadSize > 0)
            {
                OnDownloadNeeded?.Invoke();
                ShowUpdatePopUp(downloadSize);
            }
        }

        private async void ShowUpdatePopUp(long downloadSize)
        {
            GameObject updateConfirmationPoUp = await ResourceManager.InstantiateObject("UI/Pop Up/Bundle Update Confirmation.prefab");
            UpdateConfirmationPopUp updateConfirmationPopUp = updateConfirmationPoUp.GetComponent<UpdateConfirmationPopUp>();
            
            updateConfirmationPopUp.SetMessage($"Download additional file for {FileSize.ToMB(downloadSize).ToString("N2")} MB?");
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
