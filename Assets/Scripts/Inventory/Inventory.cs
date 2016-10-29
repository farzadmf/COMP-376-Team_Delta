using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Item.ItemType;

// ReSharper disable once CheckNamespace
public class Inventory : MonoBehaviour
{
    public GameObject InventoryPanel;

    public static Inventory Instance { get; private set; }

    public GameObject SlotPanel;
    public GameObject Slot;
    public GameObject HotKeyPanel;
    public GameObject HotKeySlot;
    public GameObject Item;
    public GameObject HotkeyItem;

    private const int HOT_KEY_COUNT = 4;

    private readonly Dictionary<int, Item> _slotItems   = new Dictionary<int, Item>();
    private readonly Dictionary<int, Item> _hotKeyItems = new Dictionary<int, Item>();
    private readonly List<GameObject     > _slots       = new List<GameObject>();
    private readonly List<GameObject     > _hotKeySlots = new List<GameObject>();

    // ReSharper disable once UnusedMember.Local
    private void Start()
    {
        Instance = this;
        InventoryPanel.SetActive(false);
        HotKeyPanel.SetActive(true);

        // Initialize inventory slots
        for (var index = 0; index < ItemDatabase.Instance.TotalItems; index++)
        {
            var newSlot = Instantiate(Slot);
            newSlot.transform.SetParent(SlotPanel.transform);
            newSlot.name = $"Slot{index + 1}";
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

    // ReSharper disable once UnusedMember.Local
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            InventoryPanel.SetActive(!InventoryPanel.activeSelf);

        if (Input.GetKeyDown(KeyCode.Alpha1))
            print(GetHotkeyItem(0));
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            print(GetHotkeyItem(1));
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            print(GetHotkeyItem(2));
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            print(GetHotkeyItem(3));
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
        itemData.Slot = itemObjectData.Slot;
        itemData.Item = item;
        itemData.HotKeySlot = slotIndex;

        // Set the names for the slot and item
        var slot = _hotKeySlots[slotIndex];
        slot.name = $"{item.Title} Slot";
        hotkeyItem.name = $"{item.Title} Image";

        hotkeyItem.transform.SetParent(slot.transform);
        hotkeyItem.GetComponent<Image>().sprite = item.Sprite;
        hotkeyItem.transform.position = _hotKeySlots[slotIndex].transform.position;

        // If item has not been dragged from another hot-key slot, update amount
        if (itemObjectData.HotKeySlot == -1)
        {
            if (item.Type != Consumable)
                return;

            itemObjectData.ItemAmount--;
            itemObjectData.GetComponentInChildren<Text>().text =
                    itemObjectData.ItemAmount.ToString();

            // If amount is greater than zero
            if (itemObjectData.ItemAmount > 0)
                return;

            // If amount is zero, remove the item
            _slotItems.Remove(item.Id);
            _slots[itemObjectData.Slot].name = $"Slot{itemObjectData.Slot + 1}";
            Destroy(itemObject);
            return;
        }

        // If item was dragged from another hot-key slot
        _hotKeyItems.Remove(itemObjectData.HotKeySlot);
        _hotKeySlots[itemObjectData.HotKeySlot].name = $"HotKeySlot{itemObjectData.HotKeySlot + 1}";
        Destroy(itemObject);
    }

    public void AddItem(int id)
    {
        var item = ItemDatabase.Instance.GetItem(id);
        if (item == null)
            return;

        // If we already have the item and it's not stackable
        if (_slotItems.ContainsKey(item.Id) && !item.Stackable)
            return;

        if (item.Stackable && _slotItems.ContainsKey(item.Id))
        {
            var itemData = _slots[item.Id].GetComponentInChildren<ItemData>();
            itemData.ItemAmount++;
            itemData.GetComponentInChildren<Text>().text = itemData.ItemAmount.ToString();
            return;
        }

        _slotItems.Add(item.Id, item);
        AddItemToSlot(item.Id, item);
    }

    public Item GetHotkeyItem(int hotkeyIndex)
    {
        var item = _hotKeyItems.ContainsKey(hotkeyIndex) ? _hotKeyItems[hotkeyIndex] : null;
        if (item == null)
            return null;

        if (item.Type != Consumable)
            return item;

        // Remove the item from hot-keys (we know that the slot has only a single child)
        // Also, change the name to be the default
        Destroy(_hotKeySlots[hotkeyIndex].transform.GetChild(0).gameObject);
        _hotKeySlots[hotkeyIndex].name = $"HotKeySlot{hotkeyIndex + 1}";
        _hotKeyItems.Remove(hotkeyIndex);
        return item;
    }

    private void AddItemToSlot(int slotIndex, Item item)
    {
        var itemObject = Instantiate(Item);
        var slot = _slots[slotIndex];
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
        itemObject.GetComponent<ItemData>().Slot = slotIndex;
    }
}
