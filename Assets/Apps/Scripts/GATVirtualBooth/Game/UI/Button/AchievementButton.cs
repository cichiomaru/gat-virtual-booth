using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GATVirtualBooth.Game
{
    public class AchievementButton : MonoBehaviour
    {
        [SerializeField] private string targetPath;
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(InternalShow);
        }

        private void InternalShow()
        {
            MenuManager.Instance.Show(targetPath);
        }
    }
}
