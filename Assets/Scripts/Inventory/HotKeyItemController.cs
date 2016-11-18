using UnityEngine;

// ReSharper disable once CheckNamespace
public class HotKeyItemController : MonoBehaviour
{
    public float MoveToPlayerSpeed = 5;
    public float MinDistance       = 1;
    public float MaxScale          = 3;
    public float MinScale          = 0.2f;
    public float ScaleMargin       = 0.5f;

    private bool _moveToPlayer;

    // Scaling increases first, then it decreases
    private bool _increasing;

    // ReSharper disable once UnusedMember.Local
    private void Update()
    {
        if (!_moveToPlayer)
            return;

        var player = GameObject.Find("Player");
        if (player == null)
            return;

        var playerScreenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, player.transform.position);
        transform.position = Vector2.Lerp(transform.position, playerScreenPosition, Time.deltaTime * MoveToPlayerSpeed);

        var currentScale = transform.localScale;
        var scaleFactor = _increasing ? MaxScale : MinScale;
        var targetScale = new Vector3(scaleFactor * Mathf.Sign(currentScale.x), scaleFactor, currentScale.z);
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * MoveToPlayerSpeed * 2);

        if (_increasing && Mathf.Abs(transform.localScale.y - MaxScale) < ScaleMargin)
            _increasing = false;

        // If near the player, destroy the object
        var distance = (new Vector2(transform.position.x, transform.position.y) - playerScreenPosition).sqrMagnitude;
        if (distance >= MinDistance * MinDistance)
            return;

        Destroy(gameObject);
        Inventory.Instance.UpdatePlayerWithHotKeyItem(GetComponent<ItemData>().HotKeySlot);
    }

    public void MoveToPlayer()
    {
        _moveToPlayer = true;
        _increasing = true;
    }
}