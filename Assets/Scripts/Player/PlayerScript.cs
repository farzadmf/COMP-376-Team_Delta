using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	private bool grounded;
	public Transform groundCheckTransform;
	public LayerMask groundCheckLayerMask;
	public GameObject weapon;
	private float dmg;
	private float radius = 0.2f;
	private bool canMagic;
	private bool canLifeSteal;
	public int level;
	private Animator anim;

	void Start() {
		canMagic = false;
		dmg = weapon.GetComponent<WeaponScript> ().dmg;
		anim = GetComponent<Animator>();
	}
	public void goDemonMode() {
		canMagic = true;
		canLifeSteal = true;
        //Replace with bool Can Use Sword in player Controller
        //weapon.SetActive (false);
        GetComponent<PlayerControllerScript>().CanUseSword = false;

    }
	public void goDayMode() {
		canMagic = false;
		canLifeSteal = false;

        //Replace with bool Can Use Sword in player Controller
        //weapon.SetActive (false);
        GetComponent<PlayerControllerScript>().CanUseSword = true;
        //weapon.SetActive (true);
	}
	public bool getGrounded() {
		return grounded;
	}

	void playerAnimator() {
		anim.SetBool("grounded", grounded);
        //Removed running set boolean you should just set it in the player move method based on it's input
	}

	void Update() {
		updateGroundedStatus ();
		if (Input.GetMouseButtonDown (1)) {
			if (canMagic == true && !GetComponent<PlayerControllerScript>().IsBlocking) {
				if (level == 1 || level == 2)
					shootFireball ();
				else if (level == 3)
					fireBurst ();
			}
		}
		playerAnimator ();
	}
	void fireBurst() {
		GameObject g = (GameObject)Instantiate (Resources.Load ("FireBurst"));
		Vector3 cursorPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		g.transform.position = new Vector3 (cursorPos.x, transform.position.y - 1.5f, transform.position.z);
		/*
		if (transform.localScale.x > 0)
			//g.transform.position = new Vector3 (transform.position.x+7,transform.position.y-1.5f,transform.position.z);
		else
			g.transform.position = new Vector3 (transform.position.x-7,transform.position.y-1.5f,transform.position.z);
		*/
	}
	void shootFireball() {
		GameObject g = (GameObject)Instantiate(Resources.Load ("Fireball"));
		g.transform.position = new Vector3 (transform.position.x+1,transform.position.y,transform.position.z);
		Vector3 v = new Vector3 (10, 2, 0);
		if (transform.localScale.x < 0) {
			v = new Vector3 (-v.x, v.y, 0);
			g.transform.position = new Vector3 (transform.position.x-1,transform.position.y,transform.position.z);
		}

		g.GetComponent<Rigidbody2D> ().velocity = v;
		if (level == 1)
			g.GetComponent<FireballScript>().type = "normal";
		else if (level == 2)
			g.GetComponent<FireballScript>().type = "exploding";
		//g.transform.GetChild (0).GetChild (0).GetComponent<Rigidbody2D> ().velocity = -v;
	}


	void updateGroundedStatus() {
		
		if (Physics2D.OverlapCircle (groundCheckTransform.position, radius, groundCheckLayerMask)) {
			grounded = true;
		} else
			grounded = false;

	}



}
