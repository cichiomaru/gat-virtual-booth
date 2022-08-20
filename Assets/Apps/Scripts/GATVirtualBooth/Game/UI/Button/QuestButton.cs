using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GATVirtualBooth.Game
{
    public class QuestButton : MonoBehaviour
    {
        [SerializeField] private string targetPath;
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(InternalShow);
        }
        public void InternalShow()
        {
            MenuManager.Instance.Show(targetPath);
        }
    }
}
