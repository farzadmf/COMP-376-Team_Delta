using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
public class Tooltip : MonoBehaviour
{
    private Item _item;
    private GameObject _tooltip;

    // ReSharper disable once UnusedMember.Local
    private void Start()
    {
        _tooltip = GameObject.Find("Tooltip");
        _tooltip.SetActive(false);
    }

    // ReSharper disable once UnusedMember.Local
    private void Update()
    {
        if (_tooltip.activeSelf)
            _tooltip.transform.position = Input.mousePosition;
    }

    public void Activate(Item item)
    {
        _item = item;
        ConstructDataString();
        _tooltip.SetActive(true);
    }

    public void Deactivate()
    {
        _tooltip.SetActive(false);
    }

    private void ConstructDataString()
    {
        var data = $"<b><color=yellow><size=20>{_item.Title}</size></color></b>\n\n" +
                   $"<i><color=white>{_item.Description}</color></i>\n\n" +
                   $"<color=red>Power: {_item.Stats.Power}</color>";
        _tooltip.GetComponentInChildren<Text>().text = data;
    }
}
