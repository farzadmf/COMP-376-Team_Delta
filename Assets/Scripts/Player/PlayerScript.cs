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
	void Start() {
		canMagic = true;
		dmg = weapon.GetComponent<WeaponScript> ().dmg;
	}
	public bool getGrounded() {
		return grounded;
	}

	void Update() {
		updateGroundedStatus ();
		if (Input.GetMouseButtonDown (1))
			shootFireball ();
	}


	void shootFireball() {
		GameObject g = (GameObject)Instantiate(Resources.Load ("Fireball"));
		g.transform.position = new Vector3 (transform.position.x+1,transform.position.y,transform.position.z);
		Vector3 v = new Vector3 (10, 2, 0);
		g.GetComponent<Rigidbody2D> ().velocity = v;
		//g.transform.GetChild (0).GetChild (0).GetComponent<Rigidbody2D> ().velocity = -v;
	}


	void updateGroundedStatus() {
		if (Physics2D.OverlapCircle (groundCheckTransform.position, radius, groundCheckLayerMask)) {
			grounded = true;
		} else
			grounded = false;

	}
}
