using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using System;

namespace LabGAT.SceneTransition
{
    public class Transition : MonoBehaviour
    {
        [SerializeField] float transitionDuration;

        [SerializeField] private Transform leftSide;
        [SerializeField] private Transform rightSide;
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
            OpenTransition();
        }

        public async Task CloseTransition()
        {
            leftSide.DOLocalMoveX(0, transitionDuration, true);
            rightSide.DOLocalMoveX(0, transitionDuration, true);
            await Task.Delay((int)(transitionDuration * 1000));
            TransitionClosed?.Invoke();
        }

        public async Task OpenTransition()
        {
            leftSide.DOLocalMoveX(leftAnchor.x, transitionDuration, true);
            rightSide.DOLocalMoveX(rightAnchor.x, transitionDuration, true);
            await Task.Delay((int)(transitionDuration * 1000));
            TransitionOpened?.Invoke();
        }
    }
}
