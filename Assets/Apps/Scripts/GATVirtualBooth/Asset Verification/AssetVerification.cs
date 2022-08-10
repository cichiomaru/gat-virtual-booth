using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GATVirtualBooth.AssetVerification
{
    public class AssetVerification : MonoBehaviour
    {


        private async void OnStart()
        {
            //Caching.ClearCache();
            await ResourceManager.InitializeAddressables();

            await ResourceManager.CatalogCheck();
            await ResourceManager.CatalogUpdate();

            long downloadSize = await ResourceManager.GetDownloadSize();
            await ResourceManager.UpdateBundle();
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
