using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


namespace LabGAT.SceneTransition {
    public class TransitionDim : MonoBehaviour, ISceneTransition {
        [SerializeField] private Image dimmer;
        public Canvas canvas => GetComponent<Canvas>();

        public event Action OnTransitionStart;
        public event Action OnTransitionEnd;

        private void Start() {
            OnStart();
        }
        private async void OnStart() {
            await OpenTransition();
        }

        private async Task Fade (FadeType fadeType) {
            Color color = dimmer.color;

            OnTransitionStart?.Invoke();

            do {
                color.a += (Time.deltaTime * ((int)fadeType));
                dimmer.color = color;

                await Task.Yield();
            } while (color.a > 0 && color.a < 1);

            OnTransitionEnd?.Invoke();
        }

        public async Task OpenTransition()
        {
            canvas.enabled = true;
            await Fade(FadeType.In);
            canvas.enabled = false;
        }

        public async Task CloseTransition()
        {
            canvas.enabled = true;
            await Fade(FadeType.Out);
        }
    }
}
