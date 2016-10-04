using UnityEngine;
using System.Collections;

//If you add this script to an object that doesn't have a rigidbody2d then this will add one
[RequireComponent(typeof(Rigidbody2D))]
public class BasicProjectile : MonoBehaviour {

    [SerializeField]
    private float speed;

    [SerializeField]
    private int damage;

    private Rigidbody2D thisRigidbody;

    private Vector2 direction;

	// Use this for initialization
	void Start () {
        thisRigidbody = GetComponent<Rigidbody2D>();
    }

    //Fixed update is based on a set framerate
    void FixedUpdate()
    {
        thisRigidbody.velocity = direction * speed;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //initializes initial values of the object
    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }

    //Called when the object isn't visible in the camera anymore
    void OnBecameInvisible()
    {
        //Destroy itself when out of vision
        Destroy(gameObject);
    }

    public int GetDamage()
    {
        return damage;
    }
}
