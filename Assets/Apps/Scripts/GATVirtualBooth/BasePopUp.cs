using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GATVirtualBooth
{
    public abstract class BasePopUp : MonoBehaviour, IHideShow
    {
        [SerializeField] protected Canvas canvas;
        [SerializeField] protected TMP_Text messageText;
        [SerializeField] protected Button button0;
        protected TextMeshProUGUI button0Text => button0.GetComponentInChildren<TextMeshProUGUI>();

        protected TaskCompletionSource<int> tcs;

        protected void Awake()
        {
            button0.onClick.AddListener(OnButton0Pressed);
        }

        private void OnButton0Pressed()
        {
            tcs.TrySetResult(0);
            Hide();
        }

        public void SetMessage(string message)
        {
            messageText.text = message;
        }

        public void Hide()
        {
            canvas.enabled = false;
        }

        public void Show()
        {
            canvas.enabled = true;
        }
    }
}
