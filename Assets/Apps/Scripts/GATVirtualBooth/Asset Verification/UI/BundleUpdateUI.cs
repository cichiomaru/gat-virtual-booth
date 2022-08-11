using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace GATVirtualBooth.AssetVerification
{
    public class BundleUpdateUI : MonoBehaviour, IHideShow
    {
        private AssetVerification AssetVerification => FindObjectOfType<AssetVerification>();

        [SerializeField] private TMP_Text messageText;
        [SerializeField] private Slider loadingBar;
        [SerializeField] private TMP_Text loadingProgress;


        private void OnEnable()
        {
            AssetVerification.InitializingAddressables += ShowInitializingMessage;
            AssetVerification.CheckingCatalog += ShowCheckingCatalogMessage;
            AssetVerification.OnConnectionAvailable += ShowOnConnectionAvailable;
            AssetVerification.OnConnectionUnavailable += ShowOnConnectionUnavailable;
            AssetVerification.OnDownloadNeeded += ShowOnDownloadNeeded;
        }
        
        private void OnDisable()
        {
            AssetVerification.InitializingAddressables -= ShowInitializingMessage;
            AssetVerification.CheckingCatalog -= ShowCheckingCatalogMessage;
            AssetVerification.OnConnectionAvailable -= ShowOnConnectionAvailable;
            AssetVerification.OnConnectionUnavailable -= ShowOnConnectionUnavailable;
            AssetVerification.OnDownloadNeeded -= ShowOnDownloadNeeded;
        }

        private void Start()
        {
            Hide(loadingBar.gameObject);
        }

        private void ShowMessage(string message)
        {
            messageText.text = message;
        }

        private async void ShowOnDownloadNeeded()
        {
            ShowMessage($"Updating game assets ...");
        }

        private void ShowOnConnectionUnavailable()
        {
            ShowMessage($"Connection unavailable");
        }

        private void ShowOnConnectionAvailable()
        {
            ShowMessage($"Checking bundle version ...");
        }

        private void ShowCheckingCatalogMessage()
        {
            ShowMessage($"Establish connection ...");
        }
        
        private void ShowInitializingMessage()
        {
            ShowMessage($"Initializing ...");
        }

        public void Hide(GameObject obj)
        {
            obj.SetActive(false);
        }

        public void Show(GameObject obj)
        {
            obj.SetActive(true);
        }

    }
}
