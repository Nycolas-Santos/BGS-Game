using UnityEngine;

namespace Game.Core.Scripts
{
    public interface IInteractable
    {
        GameObject gameObject { get ; } 
        public void Interact();
        public void FinishInteraction();
        public void StartInteraction();
        public void CanInteract(bool value);
    }
}
