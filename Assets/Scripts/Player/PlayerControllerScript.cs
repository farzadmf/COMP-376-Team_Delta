using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {
	public float Speed = 0f; // 350
	private float movex = 0f; // 4
	private float movey = 0f;
	private bool touchingGround;
	public float jumpHeight;
	private Rigidbody2D rigidBody;
	private bool canDoubleJump;
	public float hp;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
	}


	void Update() {
		movex = Input.GetAxis ("Horizontal");

		if (Input.GetKeyDown("space")) {
			if (GetComponent<PlayerScript> ().getGrounded () == true) {
				rigidBody.velocity = new Vector2 (rigidBody.velocity.x,0);
				rigidBody.AddForce (new Vector2 (0, jumpHeight));
				canDoubleJump = true;
			} else {
				if (canDoubleJump == true) {
					canDoubleJump = false;
					rigidBody.velocity = new Vector2(rigidBody.velocity.x,0);
					rigidBody.AddForce (new Vector2 (0, jumpHeight));
				}
			}

		}
		//movey = Input.GetAxis ("Vertical"); we'll use this later for ladders
		rigidBody.velocity = new Vector2 (movex * Speed,rigidBody.velocity.y);

		if (movex > 0)
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x,0,transform.eulerAngles.z);
		else if (movex < 0)
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x,180,transform.eulerAngles.z);
	}
}
