using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GATVirtualBooth.Game
{
    public class InteractArea : MonoBehaviour, IInteractArea
    {
        public Collider Collider => GetComponent<Collider>();
        public IInteraction Interaction => GetComponentInParent<IInteraction>();

        public void Register(IInteractible interactible)
        {
            Interaction.RegisterInteractible(interactible);
        }

        public void Unregister(IInteractible interactible)
        {
            Interaction.UnregisterInteractible(interactible);
        }

        private void OnTriggerEnter(Collider other)
        {
            IInteractible interactible = other.GetComponent<IInteractible>();

            if (interactible is not null)
            {
                Register(interactible);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            IInteractible interactible = other.GetComponent<IInteractible>();

            if (interactible is not null)
            {
                Unregister(interactible);
            }
        }
    }
}
