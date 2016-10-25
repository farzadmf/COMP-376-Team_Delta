using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public GameObject SlotPanel;
    public GameObject Slot;
    public GameObject HotKeyPanel;
    public GameObject HotKeySlot;
    public GameObject Item;
    public GameObject HotkeyItem;

    private const int HOT_KEY_COUNT = 4;

    private ItemDatabase _database;
    private readonly Dictionary<int, Item> _items       = new Dictionary<int, Item>();
    private readonly Dictionary<int, Item> _hotKeyItems = new Dictionary<int, Item>();
    private readonly List<GameObject     > _slots       = new List<GameObject>();
    private readonly List<GameObject     > _hotKeySlots = new List<GameObject>();


    private int _emptySlot;

    // ReSharper disable once UnusedMember.Local
    private void Start()
    {
        Instance = this;
        _database = GetComponent<ItemDatabase>();

        // Initialize inventory slots
        for (var i = 0; i < _database.TotalItems; i++)
        {
            var newSlot = Instantiate(Slot);
            newSlot.transform.SetParent(SlotPanel.transform);
            _slots.Add(newSlot);
        }

        // Initialize hot-key slots
        for (var index = 0; index < HOT_KEY_COUNT; index++)
        {
            var newSlot = Instantiate(HotKeySlot);
            newSlot.transform.SetParent(HotKeyPanel.transform);
            newSlot.name = $"HotKeySlot{index + 1}";
            newSlot.GetComponent<HotKeySlot>().SlotNumber = index;
            newSlot.GetComponent<HotKeySlot>().ItemDropped += OnItemDropped;
            _hotKeySlots.Add(newSlot);
        }
    }

    private void OnItemDropped(int slotIndex, GameObject itemObject)
    {
        var itemObjectData = itemObject.GetComponent<ItemData>();
        var item = itemObjectData.Item;

        if (_hotKeyItems.ContainsKey(slotIndex))
            _hotKeyItems[slotIndex] = item;
        else
            _hotKeyItems.Add(slotIndex, item);

        var hotkeyItem = Instantiate(HotkeyItem);

        var itemData = hotkeyItem.GetComponent<ItemData>();
        itemData.Item = item;
        itemData.HotKeySlot = slotIndex;

        hotkeyItem.transform.SetParent(_hotKeySlots[slotIndex].transform);
        hotkeyItem.GetComponent<Image>().sprite = item.Sprite;
        hotkeyItem.transform.position = _hotKeySlots[slotIndex].transform.position;

        if (itemObjectData.HotKeySlot == -1)
            return;

        // If item was dragged from another hot-key slot
        _hotKeyItems.Remove(itemObjectData.HotKeySlot);
        Destroy(itemObject);
    }

    public void AddItem(int id)
    {
        var item = _database.GetItem(id);
        if (item == null)
            return;

        // If we already have the item and it's not stackable
        if (_items.ContainsKey(item.Id) && !item.Stackable)
            return;

        if (item.Stackable && _items.ContainsKey(item.Id))
        {
            var itemData = _slots[item.Id].GetComponentInChildren<ItemData>();
            itemData.ItemAmount++;
            itemData.GetComponentInChildren<Text>().text = itemData.ItemAmount.ToString();
            return;
        }

        _items.Add(item.Id, item);
        var itemObject = Instantiate(Item);
        var slot = _slots[_emptySlot];
        // Set the parent and position
        itemObject.transform.SetParent(slot.transform);
        itemObject.transform.position = slot.transform.position;
        // Set the image
        itemObject.GetComponent<Image>().sprite = item.Sprite;
        // Set the name for 'slot' and 'item'
        itemObject.name = $"{item.Title} Image";
        slot.name = $"{item.Title} Slot";
        // Set the item data
        itemObject.GetComponent<ItemData>().Item = item;
        itemObject.GetComponent<ItemData>().Slot = _emptySlot;

        _emptySlot++;
    }

    public Item GetHotkeyItem(int hotkeyIndex)
    {
        return _hotKeyItems.ContainsKey(hotkeyIndex) ? _hotKeyItems[hotkeyIndex] : null;
    }

}
