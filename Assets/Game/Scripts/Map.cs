using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts
{
    public class Map : MonoBehaviour
    {
        public UnityEvent OnEnableEvent;
        public UnityEvent OnDisableEvent;
        private void OnEnable()
        {
            OnEnableEvent.Invoke();
        }

        private void OnDisable()
        {
            OnDisableEvent.Invoke();
        }
    }
}

