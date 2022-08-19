using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using System;

namespace LabGAT.SceneTransition
{
    public class TransitionShutDoor : MonoBehaviour, ISceneTransition
    {
        [SerializeField] float transitionDuration;
        public Canvas canvas => GetComponent<Canvas>();

        [SerializeField] private Transform leftSide;
        [SerializeField] private Transform rightSide;
        [SerializeField] private Transform logoCenter;
        [SerializeField] private Vector3 leftAnchor;
        [SerializeField] private Vector3 rightAnchor;


        public event Action TransitionClosed;
        public event Action TransitionOpened;

        private void Awake()
        {
            DOTween.Init();
        }

        private void Start()
        {
            OnStart();
        }

        private async void OnStart()
        {
            await OpenTransition();
        }

        private async Task SpinLogo(int direction)
        {
            logoCenter.DOLocalRotate(new Vector3(0, 0, 360) * direction, transitionDuration, RotateMode.FastBeyond360);
            await Task.Delay((int)(transitionDuration * 1000));
        }

        public async Task CloseTransition()
        {
            canvas.enabled = true;

            leftSide.DOLocalMoveX(0, transitionDuration, true);
            rightSide.DOLocalMoveX(0, transitionDuration, true);
            await Task.Delay((int)(transitionDuration * 1000));
                        
            await SpinLogo(1);

            TransitionClosed?.Invoke();
        }

        public async Task OpenTransition()
        {
            canvas.enabled = true;
            await SpinLogo(-1);

            leftSide.DOLocalMoveX(leftAnchor.x, transitionDuration, true);
            rightSide.DOLocalMoveX(rightAnchor.x, transitionDuration, true);
            await Task.Delay((int)(transitionDuration * 1000));
            TransitionOpened?.Invoke();

            canvas.enabled = false;
        }
    }
}
