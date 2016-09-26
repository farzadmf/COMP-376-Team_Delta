using UnityEngine;
using System.Collections;

//If you add this script to an object that doesn't have a rigidbody2d then this will add one
[RequireComponent(typeof(Rigidbody2D))]
public class BasicProjectile : MonoBehaviour {

    [SerializeField]
    private float speed;

    private Rigidbody2D thisRigidbody;

    private Vector2 direction;

	// Use this for initialization
	void Start () {
        thisRigidbody = GetComponent<Rigidbody2D>();
        //Default is direction right
        Initialize(Vector2.right);
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
}
