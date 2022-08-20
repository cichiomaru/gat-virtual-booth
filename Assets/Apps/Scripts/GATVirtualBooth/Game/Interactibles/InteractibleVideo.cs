using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GATVirtualBooth.Game
{
    public class InteractibleVideo : MonoBehaviour, IInteractible
    {
        [SerializeField] private VideoUIDataModel videoUIDataModel;

        private void Awake()
        {
            
        }

        public void Execute()
        {
            IWidget widget = GameplayMenuManager.instance.Show(Path.Gameplay.VideoInfo);
            widget.SetContent(videoUIDataModel);
        }

        public string GetName()
        {
            return gameObject.name;
        }
    }
}
