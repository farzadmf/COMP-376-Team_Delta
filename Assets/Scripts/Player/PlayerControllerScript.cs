﻿using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {
	public float Speed = 0f; // 350
	private float movex = 0f; // 4
	private float movey = 0f;
	public bool touchingGround;
	public float jumpHeight;
	private Rigidbody2D rigidBody;
	private bool canDoubleJump;
	public float hp;
    private Animator anim;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator>();
	}
	void Update() {
		move ();
		jump ();
		attack ();
		fixTextOrientation ();
        anim.SetBool("Grounded", touchingGround);
        anim.SetFloat("Speed", Speed);

    }

	void attack() {
		if (Input.GetMouseButtonDown (0)) { // left click
			
			if (GetComponent<PlayerScript> ().weapon.GetComponent<WeaponScript>().getAnimating() == false)
				GetComponent<PlayerScript> ().weapon.GetComponent<WeaponScript>().attack ();
		}
	}
		
	
	void jump() {
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
	}

	void move() {

		movex = Input.GetAxis ("Horizontal");
		//movey = Input.GetAxis ("Vertical"); we'll use this later for ladders
		rigidBody.velocity = new Vector2 (movex * Speed,rigidBody.velocity.y);

		if (movex > 0)
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x,0,transform.eulerAngles.z);
		else if (movex < 0)
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x,180,transform.eulerAngles.z);
            
	}

	void fixTextOrientation() {
		GameObject text = transform.FindChild ("Partner").FindChild("Text").gameObject;
		Quaternion rot = text.transform.rotation;
		if (transform.rotation.y == 0) {
			rot.eulerAngles = new Vector3 (0, 0, 0);

		} else {
			rot.eulerAngles = new Vector3 (0, -180, 0);
		}
		text.transform.localRotation = rot;
        
	}
}
