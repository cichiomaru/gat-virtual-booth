using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GATVirtualBooth.Game
{
    public class InteractibleText : MonoBehaviour, IInteractible
    {
        [SerializeField] private TextUIDataModel textUIDataModel;

        private void Awake()
        {
            //retrieve data 
        }

        public void Execute()
        {
            IWidget widget = GameplayMenuManager.instance.Show(Path.Gameplay.TextInfo);
            widget.SetContent(textUIDataModel);
        }

        public string GetName()
        {
            return gameObject.name;
        }
    }
}
