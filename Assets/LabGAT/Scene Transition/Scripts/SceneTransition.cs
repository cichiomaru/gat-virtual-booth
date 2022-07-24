using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


namespace LabGAT.SceneTransition {
    public class SceneTransition : MonoBehaviour {
        [SerializeField] private Image dimmer;

        public event Action OnTransitionStart;
        public event Action OnTransitionEnd;

        private void Start() {
            OnStart();
        }
        private async void OnStart() {
            await TransitionIn();
            await TransitionOut();
        }

        public async Task TransitionIn() {
            await Fade(FadeType.In);
        }
        public async Task TransitionOut() {
            await Fade(FadeType.Out);
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
    }
}
