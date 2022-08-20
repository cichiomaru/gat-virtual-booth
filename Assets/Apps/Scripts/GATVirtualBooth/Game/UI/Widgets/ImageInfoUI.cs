using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GATVirtualBooth.Game
{
    public class ImageInfoUI : MonoBehaviour, IWidget
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private Button buttonClose;

        public string Path => GATVirtualBooth.Path.Gameplay.ImageInfo;


        private void Awake()
        {
            canvas = GetComponent<Canvas>();
            buttonClose.onClick.AddListener(Hide);
        }

        private void Start()
        {
            Hide();
        }
       
        public void Hide()
        {
            canvas.enabled = false;
        }

        public void Show()
        {
            canvas.enabled = true;
        }

        public void SetContent(DataModel content)
        {
            ImageUIDataModel dataModel = (ImageUIDataModel)content;

            titleText.text = dataModel.title;
            image.sprite = dataModel.sprite;
        }
    }
}
