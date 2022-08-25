using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GATVirtualBooth.Game
{
    public class TextInfoUI : MonoBehaviour, IWidget
    {
        public string Path => GATVirtualBooth.Path.Gameplay.TextInfo;

        [SerializeField] private Canvas canvas;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI infoText;
        [SerializeField] private Button closeButton;


        private void Awake()
        {
            canvas = GetComponent<Canvas>();
            closeButton.onClick.AddListener(Hide);
        }

        private void Start()
        {
            Hide();
        }

        public void Hide()
        {
            canvas.enabled = false;
        }

        public void SetContent(DataModel content)
        {
            TextUIDataModel dataModel = (TextUIDataModel)content;

            titleText.text = dataModel.title;
            infoText.text = dataModel.info;
        }

        public void Show()
        {
            canvas.enabled = true;
        }
    }
}
