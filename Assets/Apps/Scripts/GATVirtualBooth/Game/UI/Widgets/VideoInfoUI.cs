using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LightShaft.Scripts;

namespace GATVirtualBooth.Game
{
    public class VideoInfoUI : MonoBehaviour, IWidget
    {
        public string Path => GATVirtualBooth.Path.Gameplay.VideoInfo;

        [SerializeField] private Canvas canvas;
        //[SerializeField] private Button closeButton;
        //[SerializeField] TextMeshProUGUI titleText;
        [SerializeField] private YoutubePlayer youtubePlayer;

        private void Awake()
        {
            canvas = GetComponent<Canvas>();
            //closeButton.onClick.AddListener(Hide);
        }

        public void Hide()
        {
            canvas.enabled = false;
        }

        public void SetContent(DataModel content)
        {
            VideoUIDataModel dataModel = (VideoUIDataModel)content;

            youtubePlayer.youtubeUrl = dataModel.url;
            youtubePlayer.PlayVideo();
        }

        public void Show()
        {
            canvas.enabled = true;
        }
    }
}
