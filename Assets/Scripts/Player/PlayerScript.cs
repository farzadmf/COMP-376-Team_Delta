using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	private bool grounded;
	public Transform groundCheckTransform;
	public LayerMask groundCheckLayerMask;
	private float radius = 0.2f;

	void Start() {
		
	}
	public bool getGrounded() {
		return grounded;
	}

	void Update() {
		updateGroundedStatus ();
	}
	void updateGroundedStatus() {
		if (Physics2D.OverlapCircle (groundCheckTransform.position, radius, groundCheckLayerMask)) {
			grounded = true;
		} else
			grounded = false;

	}
}
