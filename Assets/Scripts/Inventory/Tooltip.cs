using System;
using UnityEngine;
using UnityEngine.UI;
using static Item.ItemType;

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
                   $"<i><color=white>{_item.Description}</color></i>\n\n";

        switch (_item.Type)
        {
            case Consumable:
                data += $"<color=red>Health: </color><b><color=cyan>{_item.Stats.Health}</color></b>\n" +
                        $"<color=red>Stamina: </color><b><color=cyan>{_item.Stats.Stamina}</color></b>\n" +
                        $"<color=red>Strength: </color><b><color=cyan>{_item.Stats.Strength}</color></b>\n" +
                        $"<color=red>Defense: </color><b><color=cyan>{_item.Stats.Defense}</color></b>";
                break;
            case Weapon:
                data += $"<color=red>Base Damage: </color><b><color=cyan>{_item.Attack.BaseDamage}</color></b>\n" +
                        $"<color=red>Force: </color><b><color=cyan>{_item.Attack.Force}</color></b>";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        TooltipImage.GetComponentInChildren<Text>().text = data;
    }
}
