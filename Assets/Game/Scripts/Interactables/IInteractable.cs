using System;
using UnityEngine;

namespace Game.Scripts.Interactables
{
    public interface IInteractable
    {
        public GameObject gameObject { get ; }
        public bool LockPlayerInPlace { get; set; }
        public bool PlayerCanInteract { get; set; }

        public event Action OnInteractionFinished;
        public event Action OnInteractionStarted;
        public void Interact() { }
    }
}
