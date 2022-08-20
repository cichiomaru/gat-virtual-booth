using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GATVirtualBooth.Game
{
    public class InteractibleImage : MonoBehaviour, IInteractible
    {


        public void Execute()
        {
            Logger.Log($"Not implemented", this);
        }

        public string GetName()
        {
            return gameObject.name;
        }
    }
}
