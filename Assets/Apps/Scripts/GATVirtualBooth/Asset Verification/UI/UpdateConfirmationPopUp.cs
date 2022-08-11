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

        public event Action YesPressed;
        public event Action NoPressed;

        private void Awake()
        {
            yesButton.onClick.AddListener(OnYesPressed);
            noButton.onClick.AddListener(OnNoPressed);
        }
        private void OnYesPressed()
        {
            YesPressed?.Invoke();
        }

        private void OnNoPressed()
        {
            NoPressed?.Invoke();
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
