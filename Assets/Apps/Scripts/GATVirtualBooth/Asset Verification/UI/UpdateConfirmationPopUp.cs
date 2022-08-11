using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace GATVirtualBooth.AssetVerification
{
    public class UpdateConfirmationPopUp : MonoBehaviour, IHideShow
    {
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;

        private void Awake()
        {
            yesButton.onClick.AddListener(OnYesPressed);
            noButton.onClick.AddListener(OnNoPressed);
        }

        private async void OnYesPressed()
        {
            Hide(gameObject);
            await ResourceManager.UpdateBundle();
        }

        private void OnNoPressed()
        {

        }

        public void SetMessage(string message)
        {
            messageText.text = message;
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
