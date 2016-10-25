using UnityEngine;

// ReSharper disable once CheckNamespace
public class TestInventory : MonoBehaviour
{
    public GameObject InventoryPanel;

    // ReSharper disable once UnusedMember.Local
    private void Start() => InventoryPanel.SetActive(false);

    // ReSharper disable once UnusedMember.Local
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Inventory.Instance.AddItem(0);
        else if (Input.GetKeyDown(KeyCode.B))
            Inventory.Instance.AddItem(1);

        if (Input.GetKeyDown(KeyCode.Space))
            InventoryPanel.SetActive(!InventoryPanel.activeSelf);

        if (Input.GetKeyDown(KeyCode.Alpha1))
            print(Inventory.Instance.GetHotkeyItem(0));
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            print(Inventory.Instance.GetHotkeyItem(1));
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            print(Inventory.Instance.GetHotkeyItem(2));
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            print(Inventory.Instance.GetHotkeyItem(3));
    }
}
