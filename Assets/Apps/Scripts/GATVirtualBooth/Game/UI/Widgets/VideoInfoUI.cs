using LightShaft.Scripts;
using UnityEngine;
using LabGAT.InputSystem;

namespace GATVirtualBooth.Game
{
    public class VideoInfoUI : MonoBehaviour, IWidget
    {
        public string Path => GATVirtualBooth.Path.Gameplay.VideoInfo;

        [SerializeField] private InputSO input;
        [SerializeField] private Canvas canvas;
        //[SerializeField] private Button closeButton;
        //[SerializeField] TextMeshProUGUI titleText;
        [SerializeField] private YoutubePlayer youtubePlayer;

        private void Awake()
        {
            canvas = GetComponent<Canvas>();
            //closeButton.onClick.AddListener(Hide);
            input.OnMenuClosed += CloseUI;
        }

        private void CloseUI()
        {
            GameplayMenuManager.instance.Hide(Path);
        }

        public void Hide()
        {
            Screen.orientation = ScreenOrientation.Portrait;
            canvas.enabled = false;
        }

        public void Show()
        {
            canvas.enabled = true;
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }

        public void SetContent(DataModel content)
        {
            VideoUIDataModel dataModel = (VideoUIDataModel)content;

            youtubePlayer.youtubeUrl = dataModel.url;
            youtubePlayer.PlayVideo();
        }

        private void OnApplicationQuit()
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }
}
