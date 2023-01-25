using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.Interactables
{
    public class InteractableEnterMap : MonoBehaviour, IInteractable
    {
        [SerializeField] private AudioClip audioClip;
        
        [SerializeField] private GameObject onCanInteractUi;
        [SerializeField] private Transform mapEntrance;
        
        [SerializeField] private Map newMap;
        [SerializeField] private Map oldMap;

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
            IsInteracting = false;
            OnInteractionFinished?.Invoke();
            GameManager.Instance.Play2DAudio(audioClip);
            GameManager.Instance.Player.gameObject.transform.position = mapEntrance.position;
            GameManager.Instance.SwitchMap(newMap,oldMap);
            yield return null;
        }
        
    }
}
