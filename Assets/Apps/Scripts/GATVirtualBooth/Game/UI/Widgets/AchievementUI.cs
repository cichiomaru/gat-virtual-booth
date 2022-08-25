using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GATVirtualBooth.Game
{
    public class AchievementUI : IWidget
    {
        public string Path => "";

        public void Hide()
        {
            throw new System.NotImplementedException();
        }

        public void SetContent(DataModel content)
        {
            throw new System.NotImplementedException();
        }

        public void Show()
        {
            throw new System.NotImplementedException();
        }

        private void Start()
        {
            Hide();
        }
    }
}
