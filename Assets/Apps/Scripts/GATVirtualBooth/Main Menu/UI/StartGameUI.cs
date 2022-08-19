using LabGAT.SceneTransition;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GATVirtualBooth.Menu
{
    public class StartGameUI : MonoBehaviour
    {
        private Button StartButton => GetComponent<Button>();
        private ISceneTransition sceneTransition;

        private void Awake()
        {
            StartButton.onClick.AddListener(StartGame);
            foreach (Canvas canvas in FindObjectsOfType<Canvas>())
            {
                if (canvas.GetComponent<ISceneTransition>() is not null)
                {
                    sceneTransition = canvas.GetComponent<ISceneTransition>();
                    break;
                }
            }
        }

        private async void StartGame()
        {
            await sceneTransition.CloseTransition();
            await ResourceManager.LoadScene("Scenes/Lobby.unity", LoadSceneMode.Single);
        }
    }
}
