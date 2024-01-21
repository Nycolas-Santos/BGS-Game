using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Scripts
{
    public class InventoryController : MonoBehaviour
    {
        public GameObject inventoryGrid;
        public List<ItemSlot> itemSlots;

        private void Start()
        {
            GetAllSlots();
            UpdateAllSlots();
        }

        private void OnEnable()
        {
            GetAllSlots();
            UpdateAllSlots();
        }

        public void TryAddItemToSlot(Item item)
        {
            foreach (var slot in itemSlots)
            {
                if (slot.item == null)
                {
                    slot.item = item;
                    slot.UpdateSlotUI();
                    return;
                }
            }
        }

        public void RemoveItemFromSlot(Item item)
        {
            foreach (var slot in itemSlots)
            {
                if (slot.item == item)
                {
                    slot.item = null;
                    slot.UpdateSlotUI();
                    return;
                }
            }
        }

        private void UpdateAllSlots()
        {
            foreach (var slot in itemSlots)
            {
                slot.UpdateSlotUI();
            }
        }

        private void GetAllSlots()
        {
            for (int i = 0; i < inventoryGrid.transform.childCount; i++)
            {
                itemSlots.Add(inventoryGrid.transform.GetChild(i).GetComponent<ItemSlot>());
            }
        }
    }
}
