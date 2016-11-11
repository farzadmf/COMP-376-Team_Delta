using UnityEngine;
using UnityEngine.EventSystems;

// ReSharper disable once CheckNamespace
public class ItemData : MonoBehaviour, IPointerDownHandler, IBeginDragHandler,
    IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item Item;
    public int ItemAmount = 1;
    public int Slot       = -1;
    public int HotKeySlot = -1;

    private Transform _originalParent;
    private Vector2 _offset;
    private GameObject _parentCanvas;
    private CanvasGroup _canvasGroup;
    private Tooltip _tooltip;

    // ReSharper disable once UnusedMember.Local
    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _parentCanvas = GameObject.Find("InventoryCanvas");

        // Get the tool-tip from the inventory object
        _tooltip = Inventory.Instance.GetComponent<Tooltip>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Item == null)
            return;

        _offset = eventData.position - (Vector2) transform.position;
        transform.SetParent(_parentCanvas.transform);    // The parent Canvas
        _canvasGroup.blocksRaycasts = false;
        SetPosition(eventData.position);
    }

    public void OnPointerDown(PointerEventData eventData) => _originalParent = transform.parent;

    public void OnDrag(PointerEventData eventData)
    {
        if (Item == null)
            return;

        SetPosition(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(_originalParent);
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        _canvasGroup.blocksRaycasts = true;
    }

    private void SetPosition(Vector2 position) => transform.position = position - _offset;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // If the item is in a hot-key slot, don't bother with the tool-tip
        if (HotKeySlot != -1)
            return;

        _tooltip.Activate(Item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // If the item is in a hot-key slot, don't bother with the tool-tip
        if (HotKeySlot != -1)
            return;

        _tooltip.Deactivate();
    }
}
