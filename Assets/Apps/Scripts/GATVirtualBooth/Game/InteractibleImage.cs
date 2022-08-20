using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GATVirtualBooth.Game
{
    public class InteractibleImage : MonoBehaviour, IInteractible
    {
        [SerializeField] private ImageUIDataModel imageUIDataModel;

        private void Awake()
        {
            //set data model here
        }

        public void Execute()
        {
            IWidget widget = GameplayMenuManager.instance.Show(GameplayUIPath.ImageInfo);
            widget.SetContent(imageUIDataModel);
        }

        public string GetName()
        {
            return gameObject.name;
        }
    }
}
