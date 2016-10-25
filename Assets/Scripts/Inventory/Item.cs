 // ReSharper disable once CheckNamespace

using UnityEngine;

// ReSharper disable once CheckNamespace
public class Item
{
    public class ItemStats
    {
        public int Power { get; set; }

        public int Defense { get; set; }

        public int Vitality { get; set; }
    }

    public enum ItemType
    {
        Consumable,
        Weapon
    }

    public int Id { get; set; } = -1;

    public string Title { get; set; }

    public int Value { get; set; }

    public ItemStats Stats { get; set; }

    public ItemType Type { get; set; }

    public string Description { get; set; }

    public bool Stackable { get; set; }

    public int Rarity { get; set; }

    private int _iconIndex;
    public int IconIndex
    {
        get { return _iconIndex; }
        set
        {
            _iconIndex = value;
            var sprites = Resources.LoadAll<Sprite>("Inventory/Items/Items");
            Sprite = sprites[_iconIndex];
        }
    }

    public Sprite Sprite { get; set; }
}
