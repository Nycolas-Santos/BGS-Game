using System;
using UnityEngine;

namespace Game.Core.Scripts
{
    public class ShopController : MonoBehaviour
    {
        public static event Action OnOpenShop;
        public static event Action OnCloseShop;

        private void OnEnable()
        {
            OnOpenShop?.Invoke();
        }

        private void OnDisable()
        {
            OnCloseShop?.Invoke();
        }
    }
}
