using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Core.Scripts
{
    public class ItemSlot : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Item item;

        public Image itemImageIcon;
        public Text itemSellingPrice;

        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        public bool isEquipable;
        public ItemType equipableItemType;

        public UnityEvent OnEquipItem;
        public UnityEvent OnUnequipItem;

        private const string DEBUG_EQUIP_LOG = "Equip Item";
        private const string DEBUG_UNEQUIP_LOG = "Unequip Item";

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
                itemImageIcon.transform.SetParent(transform.parent.parent);
                itemImageIcon.transform.SetAsLastSibling();
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (item != null)
            {
                itemImageIcon.rectTransform.anchoredPosition += eventData.delta / GetComponentInParent<Canvas>().scaleFactor;
            }

            if (eventData.pointerEnter == UserInterfaceManager.Instance.ShopController.gameObject)
            {
                itemSellingPrice.text = Mathf.RoundToInt(item.price*GameManager.Instance.gameSettings.SellingItemMultiplier).ToString();
            }
            else
            {
                itemSellingPrice.text = String.Empty;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 1f; // Reset the transparency
            canvasGroup.blocksRaycasts = true; // Enable raycasts again
            
            itemImageIcon.transform.SetParent(transform);
            itemImageIcon.rectTransform.anchoredPosition = Vector2.zero;
            // Check if the pointer is over a valid drop target (another item slot)
            GameObject dropTarget = eventData.pointerEnter;

            if (dropTarget != null && dropTarget.GetComponent<ItemSlot>() != null)
            {
                ItemSlot targetSlot = dropTarget.GetComponent<ItemSlot>();
                if (targetSlot.isEquipable)
                {
                    if (item.ItemType != targetSlot.equipableItemType) return;
                    Debug.Log(DEBUG_EQUIP_LOG);
                    OnEquipItem.Invoke();
                    switch (item.ItemType)
                    {
                        case ItemType.Clothes:
                            GameManager.Instance.PlayerInstance.EquipClothes(item);
                            break;
                        case ItemType.Head:
                            GameManager.Instance.PlayerInstance.EquipHead(item);
                            break;
                        case ItemType.Weapon:
                            break;
                        case ItemType.Consumable:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else if (isEquipable)
                {
                    switch (item.ItemType)
                    {
                        case ItemType.Clothes:
                            GameManager.Instance.PlayerInstance.EquipClothes(null);
                            break;
                        case ItemType.Head:
                            GameManager.Instance.PlayerInstance.EquipHead(null);
                            break;
                        case ItemType.Weapon:
                            break;
                        case ItemType.Consumable:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    OnUnequipItem.Invoke();
                    Debug.Log(DEBUG_UNEQUIP_LOG);
                }
                // Exchange items between slots
                Item tempItem = targetSlot.item;
                targetSlot.item = item;
                item = tempItem;

                // Update UI for both slots
                UpdateSlotUI();
                targetSlot.UpdateSlotUI();
            }
            else if (dropTarget == UserInterfaceManager.Instance.ShopController.gameObject)
            {
                GameManager.Instance.PlayerInstance.Inventory.AddCoin(Mathf.RoundToInt(item.price*GameManager.Instance.gameSettings.SellingItemMultiplier));
                GameManager.Instance.PlayerInstance.UnequipItem(item);
                OnUnequipItem.Invoke();
                Debug.Log(DEBUG_UNEQUIP_LOG);

                item = null;
                UpdateSlotUI();
                return;
            }
            else
            {
                // Reset the item to its original slot if dropped outside a valid target
                UpdateSlotUI();
            }
        }
    }
}