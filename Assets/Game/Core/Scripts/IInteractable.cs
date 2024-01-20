namespace Game.Core.Scripts
{
    public interface IInteractable
    {
        public void Interact();
        public void FinishInteraction();
        public void StartInteraction();
        public void CanInteract(bool value);
    }
}
