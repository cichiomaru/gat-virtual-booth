using System.Collections.Generic;

namespace GATVirtualBooth.Game
{
    public interface IInteraction
    {
        public void Interact();
        public void RegisterInteractible(IInteractible interactible);
        public void UnregisterInteractible(IInteractible interactible);
    }
}