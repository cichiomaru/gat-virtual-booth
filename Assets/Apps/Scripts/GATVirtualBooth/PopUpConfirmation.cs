using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;

namespace GATVirtualBooth.AssetVerification
{
    public class PopUpConfirmation : BasePopUp
    {
        [SerializeField] private Button button1;
        private TextMeshProUGUI button1Text => button1.GetComponentInChildren<TextMeshProUGUI>();


        private new void Awake()
        {
            base.Awake();
            button1.onClick.AddListener(OnButton1Pressed);
        }

        private void OnButton1Pressed()
        {
            tcs.TrySetResult(1);
            Hide();
        }

        public Task<int> Show(string msg, string btn0, string btn1)
        {
            Show();

            messageText.text = msg;
            button0Text.text = btn0;
            button1Text.text = btn1;

            tcs = new TaskCompletionSource<int>();
            return tcs.Task;
        }
    }
}
