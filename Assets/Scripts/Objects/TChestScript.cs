using UnityEngine;

// ReSharper disable once InconsistentNaming
public class TChestScript : MonoBehaviour
{
    public GameObject loot;
    private bool droppedOnce;
    // Use this for initialization
    void Start()
    {
        droppedOnce = false;
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

    private void DropLoot()
    {
        loot = Instantiate(loot);
        loot.GetComponent<LevelItemController>().Item = ItemDatabase.Instance.GetItem(1);
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