 // ReSharper disable once CheckNamespace

using UnityEngine;

// ReSharper disable once CheckNamespace
public class Item
{
    public Attack Attack { get; set; }

    public enum ItemType
    {
        Consumable,
        Weapon
    }
    
    public GameObject Prefab { get; set; }

    public int Id { get; set; } = -1;

    public string Title { get; set; }

    public ItemType Type { get; set; }

    public string Description { get; set; }

    public bool Stackable { get; set; }

    private int _iconIndex;
    public int IconIndex
    {
        get { return _iconIndex; }
        set
        {
            _iconIndex = value;
            var sprites = Resources.LoadAll<Sprite>("Inventory/Items/Items");
            Sprite = sprites[_iconIndex];

            // Only weapons have prefabs
            if (Type != ItemType.Weapon)
                return;

            Prefab = Resources.Load<GameObject>($"Inventory/Prefabs/Items_{_iconIndex}");
        }
    }

    public Sprite Sprite { get; set; }
}
