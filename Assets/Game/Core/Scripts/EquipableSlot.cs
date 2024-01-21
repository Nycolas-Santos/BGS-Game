using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Core.Scripts
{
    public class EquipableSlot : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Item item;

        public Image itemImageIcon;

        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            UpdateSlotUI();
        }

        // Update the UI of the slot
        public void UpdateSlotUI()
        {
            if (item != null)
            {
                itemImageIcon.gameObject.SetActive(true);
                itemImageIcon.sprite = item.icon;
            }
            else
            {
                itemImageIcon.gameObject.SetActive(false);
                itemImageIcon.sprite = null;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // You can implement any additional logic when the pointer is pressed on the item slot
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (item != null)
            {
                canvasGroup.alpha = 0.6f; // Make the dragged item slightly transparent
                canvasGroup.blocksRaycasts = false; // Disable raycasts on the item slot while dragging
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (item != null)
            {
                itemImageIcon.rectTransform.anchoredPosition += eventData.delta / GetComponentInParent<Canvas>().scaleFactor;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 1f; // Reset the transparency
            canvasGroup.blocksRaycasts = true; // Enable raycasts again
            
            itemImageIcon.rectTransform.anchoredPosition = Vector2.zero;
            // Check if the pointer is over a valid drop target (another item slot)
            GameObject dropTarget = eventData.pointerEnter;

            if (dropTarget != null && dropTarget.GetComponent<ItemSlot>() != null)
            {
                ItemSlot targetSlot = dropTarget.GetComponent<ItemSlot>();

                // Exchange items between slots
                Item tempItem = targetSlot.item;
                targetSlot.item = item;
                item = tempItem;

                // Update UI for both slots
                UpdateSlotUI();
                targetSlot.UpdateSlotUI();
            }
            else
            {
                // Reset the item to its original slot if dropped outside a valid target
                UpdateSlotUI();
            }
        }
    }
}
