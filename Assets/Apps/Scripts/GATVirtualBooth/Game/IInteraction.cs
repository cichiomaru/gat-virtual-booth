using System.Collections.Generic;

namespace GATVirtualBooth.Game
{
    public interface IInteraction
    {
        public List<IInteractible> Interactibles { get; }

        public void Interact();
        public void RegisterInteractible(IInteractible interactible);
        public void UnregisterInteractible(IInteractible interactible);
    }
}