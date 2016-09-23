using UnityEngine;
using System.Collections;

public class PlayerScipt : MonoBehaviour {
	public float Speed = 0f;
	public Vector2 jumpHeight;
	private float movex = 0f;
	private float movey = 0f;
	private bool touchingGround;
	// Use this for initialization
	void Start () {
	
	}


	void FixedUpdate() {
		movex = Input.GetAxis ("Horizontal");
		if (Input.GetKeyDown("space")) {
			Debug.Log ("fire");
			GetComponent<Rigidbody2D> ().AddForce (jumpHeight * Time.deltaTime);
		}
		//movey = Input.GetAxis ("Vertical"); we'll use this later for ladders
		GetComponent<Rigidbody2D>().velocity = new Vector2 (movex * Speed, movey * Speed);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
