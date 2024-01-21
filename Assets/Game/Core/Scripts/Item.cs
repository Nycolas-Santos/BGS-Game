using UnityEngine;

namespace Game.Core.Scripts
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        public string itemName = "New Item";
        public Sprite icon; // Icon for the item
        public int quantity = 1; // Quantity of the item
        public ItemType ItemType;
        public SpriteData SpriteData;
        public int price;

    }

    public enum ItemType
    {
        Clothes,
        Head,
        Weapon,
        Consumable
    }
}