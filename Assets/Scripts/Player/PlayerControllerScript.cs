using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class PlayerControllerScript : Character
{
	public float Speed = 0f; // 350
	private float movex = 0f; // 4
	private float movey = 0f;
	public float jumpHeight;
	private Rigidbody2D rigidBody;
	private bool canDoubleJump;
	public float hp;
	public CheckpointScript[] checkpoints;
	public string levelString;
	public BoxCollider2D rightCollider;
	public BoxCollider2D leftCollider;
    public BoxCollider2D bodyCollider;
	private bool canMoveX;
	private LayerMask groundCheckLayerMask;
    // Use this for initialization
    public override void Start () {
		SaveLoadScript.LoadGame ();
		CharacterStats stats = SaveLoadScript.playerStats;
		// check if game was saved at least once, otherwise we put the player stats to null
		if (SaveLoadScript.saved == true) {
			base.characterStats = stats;
}

		canMoveX = true;
        rigidBody = GetComponent<Rigidbody2D>();
        //Call the Parent's class start method
        base.Start();
		groundCheckLayerMask = LayerMask.GetMask ("Ground", "UndergroundGround");
    }

    protected override void Update()
    {
        base.Update();

		checkMoveX ();
		move ();
		jump ();
		attack ();
		fixTextOrientation ();
		checkDeathAndRestart ();

    }
	void checkDeathAndRestart() {
		bool dead = false;
		if (base.characterStats.Health <= 0)
			dead = true;
		if (dead) {
			SceneManager.LoadScene (getWholeLevelSceneString ());
		}
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
		if (canMoveX)
			rigidBody.velocity = new Vector2 (movex * Speed,rigidBody.velocity.y);

		float x = transform.localScale.x;
		if (movex > 0 && x < 0)
			transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		else if (movex < 0 && x > 0)
			transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            
	}

	void fixTextOrientation() {
		GameObject text = transform.FindChild ("Partner").FindChild("Canvas").FindChild("Text").gameObject;
		Quaternion rot = text.transform.rotation;

		if (transform.localScale.x > 0) {
			rot.eulerAngles = new Vector3 (0, 0, 0);

		} else {
			rot.eulerAngles = new Vector3 (0, -180, 0);
		}
		text.transform.localRotation = rot;
        
	}
	string getWholeLevelSceneString() {
		int latestCheckpoint = 0;
		for (int i = 0; i < checkpoints.Length; ++i) {
			int t = checkpoints[i].numberTag;
			if (t > latestCheckpoint && checkpoints[i].activated == true) {
				latestCheckpoint = t;
			}

		}
		string s = levelString + latestCheckpoint;
		return s;
	}
	void smoothTerrain(Collision2D coll) {
		for ( int i = 0; i < coll.contacts.Length; ++i) {
			ContactPoint2D p = coll.contacts[i];

			Vector3 pos = transform.position;
			if (p.normal.x <= -0.9f && p.normal.x >= -1.0f && p.normal.y == 0f)
				pos.x += 0.08f;
			else if (p.normal.x <= 1.0f && p.normal.x >= 0.9f && p.normal.y == 0f)
				pos.x -= 0.08f;
			transform.position = pos;
		}
	}
	public override void OnTriggerEnter2D(Collider2D c) {
		
        if (bodyCollider.IsTouching(c))
        {
            base.OnTriggerEnter2D(c);
        }
		
		if (c.gameObject.tag == "Ground" && (rightCollider.IsTouching (c) || (leftCollider.IsTouching (c)))) {
			rigidBody.velocity = new Vector2 (0, rigidBody.velocity.y);
			canMoveX = false;
		}
	}

	void checkMoveX() {
		if (!rightCollider.IsTouchingLayers(groundCheckLayerMask) && 
			!leftCollider.IsTouchingLayers(groundCheckLayerMask)) {
			canMoveX = true;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		
		if (coll.gameObject.tag == "Ground")
			smoothTerrain (coll);
	}
}
