using System;
using UnityEngine;
using UnityEngine.EventSystems;

// ReSharper disable once CheckNamespace
public class HotKeySlot : MonoBehaviour, IDropHandler
{
    public int SlotNumber;

    internal Action<int, GameObject> ItemDropped;

    public void OnDrop(PointerEventData eventData)
    {
        ItemDropped?.Invoke(SlotNumber, eventData.pointerDrag);
    }
}
