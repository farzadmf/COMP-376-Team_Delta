using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
public class Tooltip : MonoBehaviour
{
    public GameObject TooltipImage;

    private Item _item;

    // ReSharper disable once UnusedMember.Local
    private void Start()
    {
        TooltipImage.SetActive(false);
    }

    // ReSharper disable once UnusedMember.Local
    private void Update()
    {
        if (TooltipImage.activeSelf)
            TooltipImage.transform.position = Input.mousePosition;
    }

    public void Activate(Item item)
    {
        _item = item;
        ConstructDataString();
        TooltipImage.SetActive(true);
    }

    public void Deactivate()
    {
        TooltipImage.SetActive(false);
    }

    private void ConstructDataString()
    {
        var data = $"<b><color=yellow><size=20>{_item.Title}</size></color></b>\n\n" +
                   $"<i><color=white>{_item.Description}</color></i>\n\n" +
                   $"<color=red>Type: {_item.Type}</color>";
        TooltipImage.GetComponentInChildren<Text>().text = data;
    }
}
