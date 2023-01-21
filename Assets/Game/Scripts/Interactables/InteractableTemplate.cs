using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.Interactables
{
    public class InteractableTemplate : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject onCanInteractUi;
        
        public bool LockPlayerInPlace { get; set; } = true;
        public bool PlayerCanInteract { get; set; }
        public bool IsInteracting { get; set; }
        public event Action OnInteractionFinished;
        public event Action OnInteractionStarted;

        private void Update()
        {
            if (PlayerCanInteract == true && IsInteracting == false)
            {
                onCanInteractUi.SetActive(true);
            }
            else if (PlayerCanInteract == false || IsInteracting == true)
            {
                onCanInteractUi.SetActive(false);
            }
        }

        public void Interact()
        {
            IsInteracting = true;
            OnInteractionStarted?.Invoke();
            
            StartCoroutine(nameof(IEInteract));
        }
        private IEnumerator IEInteract()
        {
            yield return new WaitForSeconds(5);
            IsInteracting = false;
            OnInteractionFinished?.Invoke();
        }
    }
}
