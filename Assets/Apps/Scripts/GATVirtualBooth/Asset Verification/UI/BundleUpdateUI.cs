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

            ResourceManager.UpdateBundleStarted += ShowDownloadProgress;
            ResourceManager.UpdateBundleProgress += UpdateProgressBar;
            //ResourceManager.UpdateBundleFailed
            ResourceManager.UpdateBundleCompleted += ShowDownloadCompleted;
        }

        private void OnDisable()
        {
            ResourceManager.UpdateBundleStarted -= ShowDownloadProgress;
            ResourceManager.UpdateBundleProgress -= UpdateProgressBar;
            //ResourceManager.UpdateBundleFailed
            ResourceManager.UpdateBundleCompleted -= ShowDownloadCompleted;
        }

        private void Start()
        {
            HideDownloadProgress();
        }

        private void ShowDownloadCompleted()
        {
            SetMessage($"Download completed.");
        }

        private void UpdateProgressBar(float percent)
        {
            loadingBar.value = percent;
            loadingProgress.text = $"{percent * 100}%";
        }

        private void SetMessage(string message)
        {
            messageText.text = message;
        }

        private void ShowDownloadProgress()
        {

        }

        private void HideDownloadProgress()
        {

        }

        private void ShowOnDownloadNeeded()
        {
            SetMessage($"Updating game assets ...");
        }

        private void ShowOnConnectionUnavailable()
        {
            SetMessage($"Connection unavailable");
        }

        private void ShowOnConnectionAvailable()
        {
            SetMessage($"Checking bundle version ...");
        }

        private void ShowCheckingCatalogMessage()
        {
            SetMessage($"Establish connection ...");
        }
        
        private void ShowInitializingMessage()
        {
            SetMessage($"Initializing ...");
        }

        public void Hide()
        {

        }

        public void Show()
        {

        }

    }
}
