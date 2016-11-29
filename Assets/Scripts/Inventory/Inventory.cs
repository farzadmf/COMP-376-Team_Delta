using System;
using System.Collections.Generic;
using System.Globalization;
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
    public GameObject ItemEffect;

    public Text HealthText;
    public Text StaminaText;
    public Text DefenseText;
    public Text CritChanceText;
    public Text CritMultiplierText;
    public Text MaxForceResistText;

    private const int HOT_KEY_COUNT = 4;

    private readonly Dictionary<int, Item> _slotItems = new Dictionary<int, Item>();
    private readonly Dictionary<int, Item> _hotKeyItems = new Dictionary<int, Item>();
    private readonly List<GameObject> _slots = new List<GameObject>();
    private readonly List<GameObject> _hotKeySlots = new List<GameObject>();

    private GameObject _player;
    private GameObject _itemEffect;
    private PlayerScript _playerScript;
    private PlayerControllerScript _playerControllerScript;
    private Tooltip _tooltip;

    // ReSharper disable once UnusedMember.Local
    private void Start()
    {
        Instance = this;
        InventoryPanel.SetActive(false);
        HotKeyPanel.SetActive(true);

        // Get the player object
        _player = GameObject.Find("Player");
        _playerScript = _player.GetComponent<PlayerScript>();
        _playerControllerScript = _player.GetComponent<PlayerControllerScript>();

        if (_player == null)
            Debug.LogError($"[{GetType().Name}] --> Player game object is null");

        // Store a reference to the tooltip
        _tooltip = GetComponent<Tooltip>();

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

        // After initialization, we add a weapon to the inventory and equip the player with it
        const int itemId = 1;
        AddItem(itemId);

        //Sorry Disabled For Now Since Attacks are weapon dependent 
        //UpdatePlayerWithItem(ItemDatabase.Instance.GetItem(itemId));

    }

    // ReSharper disable once UnusedMember.Local
    private void Update()
    {
        // Update player stats
        var playerStats = _playerControllerScript.characterStats;
        HealthText.text = playerStats.Health.ToString();
        StaminaText.text = playerStats.Stamina.ToString();
        DefenseText.text = playerStats.Defense.ToString();
        CritChanceText.text = playerStats.CritChance.ToString(CultureInfo.InvariantCulture);
        CritMultiplierText.text = playerStats.CritMultiplier.ToString(CultureInfo.InvariantCulture);
        MaxForceResistText.text =
                playerStats.MaxForceResistence.ToString(CultureInfo.InvariantCulture);

        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryPanel.SetActive(!InventoryPanel.activeSelf);

            if (!InventoryPanel.activeSelf)
                _tooltip.Deactivate();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            UseHotKeyItem(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            UseHotKeyItem(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            UseHotKeyItem(3);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            UseHotKeyItem(4);
    }

    private void OnItemDropped(int slotIndex, GameObject itemObject)
    {
        var itemObjectData = itemObject.GetComponent<ItemData>();
        var item = itemObjectData.Item;

        // Avoid adding the item to a hot-key that slot already contains an item
        if (_hotKeyItems.ContainsKey(slotIndex))
            return;

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
        var hotKeyItemImage = hotkeyItem.GetComponent<Image>();
        SetItemImage(hotKeyItemImage, item);
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

    private static void SetItemImage(Image itemImageObject, Item item)
    {
        itemImageObject.sprite = item.Sprite;
        itemImageObject.type = Image.Type.Filled;
        itemImageObject.preserveAspect = true;

        if (!item.FlipIcon)
            return;

        var imageScale = itemImageObject.transform.localScale;
        imageScale.x = -imageScale.x;
        itemImageObject.transform.localScale = imageScale;
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

    private Item GetHotkeyItem(int hotkeyIndex)
    {
        var item = _hotKeyItems.ContainsKey(hotkeyIndex) ? _hotKeyItems[hotkeyIndex] : null;
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
        var itemObjectImage = itemObject.GetComponent<Image>();
        SetItemImage(itemObjectImage, item);
        // Set the name for 'slot' and 'item'
        itemObject.name = $"{item.Title} Image";
        slot.name = $"{item.Title} Slot";
        // Set the item data
        itemObject.GetComponent<ItemData>().Item = item;
        itemObject.GetComponent<ItemData>().Slot = slotIndex;
    }

    private void UseHotKeyItem(int hotKeyIndex)
    {
        if (!_hotKeyItems.ContainsKey(hotKeyIndex))
            return;

        var hotKeyObject = _hotKeySlots[hotKeyIndex];
        Transform hotKeyImage;

        if (_hotKeyItems[hotKeyIndex].Type == Weapon)
        {
            var originalImage = hotKeyObject.transform.GetChild(0);
            hotKeyImage = Instantiate(originalImage);
            hotKeyImage.transform.position = originalImage.transform.position;
            hotKeyImage.transform.SetParent(hotKeyObject.transform);
        }
        else
            hotKeyImage = hotKeyObject.transform.GetChild(0);
            
        var controller = hotKeyImage.GetComponent<HotKeyItemController>();
        controller.MoveToPlayer();
    }

    public void UpdatePlayerWithHotKeyItem(int hotKeyIndex)
    {
        var item = GetHotkeyItem(hotKeyIndex);
        if (item == null)
            return;

        switch (item.Type)
        {
            case Consumable:
                UpdatePlayerWithConsumable(item);

                // Remove the item from hot-keys (we know that the slot has only a single child)
                // Also, change the name to be the default
                Destroy(_hotKeySlots[hotKeyIndex].transform.GetChild(0).gameObject);
                _hotKeySlots[hotKeyIndex].name = $"hotKeySlot{hotKeyIndex + 1}";
                _hotKeyItems.Remove(hotKeyIndex);

                break;
            case Weapon:
                UpdatePlayerWithWeapon(item);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        _itemEffect = (GameObject) Instantiate(ItemEffect, _player.transform.position, Quaternion.identity);
        Destroy(_itemEffect, 1);
    }

    private void UpdatePlayerWithWeapon(Item item)
    {
        var weaponPosition = _playerScript.weapon.transform.position;

        // Delete the old weapon object
        Destroy(_playerScript.weapon.gameObject);

        // Instantiate weapon prefab at weapon's location and set the parent
        var newWeapon = (GameObject) Instantiate(item.Prefab, weaponPosition, Quaternion.identity);
        newWeapon.transform.SetParent(_player.transform);
        newWeapon.transform.localScale = Vector3.one * 1.5f;

        // Adjust weapon properties
        newWeapon.name = "Weapon";
        var damageDealerScript = newWeapon.GetComponent<DamageDealer>();
        damageDealerScript.Attack = item.Attack;

        // Set this weapon as player's weapon
        _playerScript.weapon = newWeapon;
    }

    private void UpdatePlayerWithConsumable(Item item)
    {
        var currentHealth   = _playerControllerScript.characterStats.Health;
        var currentStamina  = _playerControllerScript.characterStats.Stamina;
        var currentStrength = _playerControllerScript.characterStats.Strength;
        var currentDefense  = _playerControllerScript.characterStats.Strength;

        var newHealth   = currentHealth + item.Stats.Health;
        var newStamina  = currentStamina + item.Stats.Stamina;
        var newStrength = currentStrength + item.Stats.Strength;
        var newDefense  = currentDefense + item.Stats.Defense;

        _playerControllerScript.characterStats.Health =
                Mathf.Min(newHealth, _playerControllerScript.characterStats.TotalHealth);

        _playerControllerScript.characterStats.Stamina =
                Mathf.Min(newStamina, _playerControllerScript.characterStats.TotalStamina);

        _playerControllerScript.characterStats.Strength = newStrength;
        _playerControllerScript.characterStats.Defense = newDefense;
    }
}