using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GATVirtualBooth.Game
{
    public class InteractibleImage : MonoBehaviour, IInteractible
    {
        [SerializeField] private Sprite sprite;
        [SerializeField] private string title;

        public void Execute()
        {
            IWidget widget = GameplayMenuManager.instance.Show(GameplayUIPath.ImageInfo);
            
        }

        public string GetName()
        {
            return gameObject.name;
        }
    }
}
