using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {
	public float dmg;
	private bool animating;
	private bool forward;
	private bool finishedAnimation;
	private float wepSpeed;
	private Vector3 originalPos;
	// Use this for initialization
	void Start () {
		
		wepSpeed = 0.1f;
		originalPos = transform.localPosition;
		forward = true;
		animating = false;
		finishedAnimation = true;
	}
	void OnCollisionEnter2D() {
		Debug.Log ("collision");
	}
	void OnTriggerEnter2D(Collider2D c) {
		if (animating == true) {
			if (c.gameObject.tag == "Enemy")
				Debug.Log ("poke");
		}
	}
	void stab() {
		if (animating == true) {
			transform.localEulerAngles = new Vector3 (0,0,322f);
			if (forward == true) {
				transform.Translate (new Vector3 (wepSpeed, wepSpeed - 0.02f, 0));
			} else {
				transform.Translate (new Vector3 (-wepSpeed, -wepSpeed + 0.02f, 0));
			}
		}
	}
	void Update () {
		stab ();
		if (animating == false) {
			GetComponent<PolygonCollider2D> ().enabled = false;
		} else {
			GetComponent<PolygonCollider2D> ().enabled = true;
		}
	}
	void cancelAttackAnimation() {
		animating = false;
		transform.localEulerAngles = new Vector3 (0,0,0);
		transform.localPosition = originalPos;

	}
	void bringWepBack() {
		forward = false;
	}
	public void attack() {
		animating = true;
		forward = true;
		Invoke ("bringWepBack", 0.1f);
		Invoke ("cancelAttackAnimation",0.2f);
	}
	public bool getAnimating() {
		return animating;
	}
}
