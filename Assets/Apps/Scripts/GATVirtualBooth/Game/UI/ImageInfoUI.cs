using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GATVirtualBooth.Game
{
    public class ImageInfoUI : MonoBehaviour, IWidget
    {
        #region component
        [SerializeField] private Canvas canvas;
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private Button buttonClose;
        #endregion

        public string Path => GameplayUIPath.ImageInfo;


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

        public void Set(string title, Sprite sprite)
        {
            SetTitle(title);
            SetImage(sprite);
        }

        public void SetTitle(string title)
        {
            titleText.text = title;
        }

        public void SetImage (Sprite sprite)
        {
            image.sprite = sprite;
        }

    }
}
