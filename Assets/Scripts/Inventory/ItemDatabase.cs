using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance { get; private set; }

    private readonly Dictionary<int, Item> _database = new Dictionary<int, Item>();

    private readonly Dictionary<Item.ItemType, List<int>> _itemsByType =
            new Dictionary<Item.ItemType, List<int>>();

    public int TotalItems => _database.Count;

    // ReSharper disable once UnusedMember.Local
    private void Awake()
    {
        Instance = this;

        var filePath = $"{Application.dataPath}/StreamingAssets/items.json";
        var items = JsonConvert.DeserializeObject<List<Item>>(File.ReadAllText(filePath));

        foreach (var item in items)
        {
            _database.Add(item.Id, item);
            
            if (!_itemsByType.ContainsKey(item.Type))
                _itemsByType.Add(item.Type, new List<int>());

            _itemsByType[item.Type].Add(item.Id);
        }
    }

    public Item GetItem(int id) => _database.ContainsKey(id) ? _database[id] : null;

    public int[] GetItemIdsByType(Item.ItemType itemType) =>
            _itemsByType.ContainsKey(itemType) ? _itemsByType[itemType].ToArray() : new int[] {};
}
