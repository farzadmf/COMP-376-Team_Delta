using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//If you add this script to an object that doesn't have a rigidbody2d then this will add one
[RequireComponent(typeof(Rigidbody2D))]
public class BasicProjectile : MonoBehaviour {

    [SerializeField]
    private float speed;

    //Tags of damage sources
    [SerializeField]
    private List<string> damageSources;

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


    //For Collisions
    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        //If character can take damage from this source
        if (damageSources.Contains(other.gameObject.tag))
        {
            Destroy(gameObject);
        }
    }

    //For Trigger Collisions
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        //If character can take damage from this source
        if (damageSources.Contains(other.gameObject.tag))
        {
            Destroy(gameObject);
        }
    }

    //Called when the object isn't visible in the camera anymore
    void OnBecameInvisible()
    {
        //Destroy itself when out of vision
        Destroy(gameObject);
    }

}
