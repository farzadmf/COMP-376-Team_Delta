using UnityEngine;

// ReSharper disable once CheckNamespace
public class LevelItemController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    internal Item Item { get; set; }

    // ReSharper disable once UnusedMember.Local
    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _spriteRenderer.sprite = Item.Sprite;
    }

    // ReSharper disable once UnusedMember.Local
    private void Update()
    {
        _spriteRenderer.sprite = Item?.Sprite;
    }

    // ReSharper disable once UnusedMember.Local
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player") || Item == null)
            return;

        Inventory.Instance.AddItem(Item.Id);
        Destroy(gameObject);
    }
}