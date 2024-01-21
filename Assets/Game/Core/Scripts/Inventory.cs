using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Core.Scripts
{
    public class Inventory : MonoBehaviour
    {
        public List<Item> items = new List<Item>();
        public event Action<Item> OnAddItem;
        public event Action<Item> OnRemoveItem;
        public event Action OnReachedFullCapacity; 
        public event Action<int> OnCoinChange;
        public int startingCoins = 0;
        public List<Item> startingItems;
        public int capacity = 16;
        public int coin { get; set; }

        private const int MINIMUM_COIN_AMOUNT = 0;
        public const string REACHED_FULL_INVENTORY_CAPACITY = "You have no inventory space";

        public bool HasInventoryCapacity()
        {
            return items.Count < capacity;
        }

        private void Start()
        {
            LateStart();
        }

        private async void LateStart() // DELAYING 1 FRAME BECAUSE OF INITIALIZATION OF SINGLETONS
        {
            await Task.Yield();
            AddCoin(startingCoins);
            foreach (var item in startingItems)
            {
                AddItem(item);
            }
        }

        // Add item to the inventory
        public void AddItem(Item item)
        {
            if (items.Count == capacity)
            {
                OnReachedFullCapacity?.Invoke();
                Debug.Log(REACHED_FULL_INVENTORY_CAPACITY);
                return;
            }
            items.Add(item);
            OnAddItem?.Invoke(item);
        }

        public void AddCoin(int amount)
        {
            coin += amount;
            coin = Mathf.Clamp(coin, MINIMUM_COIN_AMOUNT, int.MaxValue);
            OnCoinChange?.Invoke(coin);
        }

        public void RemoveCoin(int amount)
        {
            coin -= amount;
            coin = Mathf.Clamp(coin, MINIMUM_COIN_AMOUNT, int.MaxValue);
            OnCoinChange?.Invoke(coin);
        }

        // Remove item from the inventory
        public void RemoveItem(Item item)
        {
            if (items.Contains(item) == false) return;
            items.Remove(item);
            OnRemoveItem?.Invoke(item);
        }

        // Use this function to remove a specific quantity of an item from the inventory
        public void RemoveItem(Item item, int quantity)
        {
            int index = items.IndexOf(item);
            if (index != -1)
            {
                items[index].quantity -= quantity;
                if (items[index].quantity <= 0)
                {
                    items.RemoveAt(index);
                }
            }
        }
    }
}