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
            }
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

        private void Start()
        {
            OnStart();
        }

        private void OnEnable()
        {

        }

        private void OnDisable()
        {

        }
    }
}
