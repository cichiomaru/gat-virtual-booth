using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace GATVirtualBooth.Game
{
    public class InteractButton : MonoBehaviour
    {
        [SerializeField] private Player player;

        #region component
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI label;
        #endregion

        #region unity function
        private void Awake()
        {
            player = FindObjectOfType<Player>();
        }

        private void Start()
        {
            HideButton();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(player.Interact);

            player.OnRegisterInteractible += SetButtonLabel;
            player.OnUnregisterInteractible += SetButtonLabel;
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(player.Interact);

            player.OnRegisterInteractible -= SetButtonLabel;
            player.OnUnregisterInteractible -= SetButtonLabel;
        }
        #endregion


        private void SetButtonLabel(IInteractible interactible)
        {
            if (interactible is null)
            {
                HideButton();
                return;
            }

            ShowButton();
            label.text = interactible.GetName();
        }

        private void ShowButton()
        {
            canvasGroup.alpha = 1;
        }

        private void HideButton()
        {
            canvasGroup.alpha = 0;
        }
    }
}
