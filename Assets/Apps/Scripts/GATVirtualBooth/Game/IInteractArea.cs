using UnityEngine;

namespace GATVirtualBooth.Game
{
    public interface IInteractArea
    {
        public Collider Collider { get; }

        public void Register(IInteractible interactible);
        public void Unregister(IInteractible interactible);
    }
}