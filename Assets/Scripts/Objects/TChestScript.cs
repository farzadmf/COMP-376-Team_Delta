using UnityEngine;

// ReSharper disable once CheckNamespace
// ReSharper disable once InconsistentNaming
public class TChestScript : MonoBehaviour
{
    public GameObject loot;
    public int ItemId;

    private bool droppedOnce;

    // ReSharper disable once UnusedMember.Local
    private void Start()
    {
        droppedOnce = false;
    }

    // ReSharper disable once UnusedMember.Local
    private void Update()
    {
        if (loot == null)
            return;

        var velocity = loot.GetComponent<Rigidbody2D>().velocity;
        var colliderComponent = loot.GetComponent<Collider2D>();
        colliderComponent.isTrigger = velocity.y > 0;
    }

    public void activatedChest()
    {
        int random = Random.Range(0, 1);
        if (random == 0)
        {
            if (droppedOnce == false)
            {
                DropLoot();
                droppedOnce = true;
            }
        }
        else
            goMimic();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sword" || other.gameObject.tag == "FireBlast")
        {
            activatedChest();
        }
    }

    private void DropLoot()
    {
        loot = Instantiate(loot);
        loot.GetComponent<LevelItemController>().Item = ItemDatabase.Instance.GetItem(ItemId);
        loot.transform.position = transform.position;
        var body = loot.GetComponent<Rigidbody2D>();
        body.velocity = new Vector2(0, 6f);
        body.gravityScale = 1;
        body.freezeRotation = true;
        Destroy(gameObject);
        if (loot.GetComponent<WeaponScript>())
        {
            loot.GetComponent<WeaponScript>().onGround = true;
        }
    }

    void goMimic() { }
}