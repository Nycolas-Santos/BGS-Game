using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core.Scripts
{
    public class ItemToBuySlot : MonoBehaviour
    {
        public Item item;
        public Image itemIcon;
        public Text itemPriceText;

        private const string PURCHASE_OK = "You purchased an item!";
        private const string PURCHASE_NOK_COIN = "You dont have the coin";

        private void OnEnable()
        {
            if (item != null)
            {
                itemIcon.sprite = item.icon;
                itemPriceText.text = item.price.ToString();
            }
        }

        public void TryBuyItem()
        {
            if (GameManager.Instance.PlayerInstance.Inventory.HasInventoryCapacity())
            {
                if (GameManager.Instance.PlayerInstance.Inventory.coin >= item.price)
                {
                    GameManager.Instance.PlayerInstance.Inventory.RemoveCoin(item.price);
                    GameManager.Instance.PlayerInstance.Inventory.AddItem(item);
                    SoundManager.Instance.PlayUISound(SoundManager.Instance.audioSettings.uiBuySound);
                    Debug.Log(PURCHASE_OK);
                    
                }
                else
                {
                    Debug.Log(PURCHASE_NOK_COIN);
                    SoundManager.Instance.PlayUISound(SoundManager.Instance.audioSettings.uiNotEnoughCashStranger);
                }
            }
            else
            {
                Debug.Log(Inventory.REACHED_FULL_INVENTORY_CAPACITY);
            }
        }
    }
}
