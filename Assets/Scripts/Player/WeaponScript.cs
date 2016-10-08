using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {
	public float dmg;
	private bool animating;
	private bool firstPart;
	private bool finishedAnimation;
	private float wepSpeed;
	private Vector3 originalPos;
	public GameObject handle;
	private int attackStyle;
	public bool onGround;
	// Use this for initialization
	void Start () {
		
		wepSpeed = 0.1f;
		originalPos = transform.localPosition;
		firstPart = true;
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
			if (c.gameObject.tag == "TChest") {
				c.gameObject.GetComponent<TChestScript> ().activatedChest ();
			}
		}
	}
	void stab() {
		
		transform.localEulerAngles = new Vector3 (0,0,322f);
		if (firstPart == true) {
			transform.Translate (new Vector3 (wepSpeed, wepSpeed - 0.02f, 0));
		} else {
			transform.Translate (new Vector3 (-wepSpeed, -wepSpeed + 0.02f, 0));
		}
	}
	void slash() {
		if (firstPart == true) {
			transform.Translate (new Vector3 (0.05f, 0.1f, 0));
			transform.RotateAround (handle.transform.position, new Vector3 (0, 0, 1), 6f);
		} else {
			transform.Translate (new Vector3 (0, -0.2f, 0));
			transform.RotateAround (handle.transform.position, new Vector3 (0, 0, 1), -16f);
		}
	}
	void Update () {
		if (animating == true) {
			
			if (attackStyle == 0)
				stab ();
			else if (attackStyle == 1)
				slash ();
		}
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
		firstPart = false;
	}
	public void attack() {
		attackStyle = Random.Range (0, 2);
		animating = true;
		firstPart = true;
		Invoke ("bringWepBack", 0.1f);
		Invoke ("cancelAttackAnimation",0.2f);
	}
	public bool getAnimating() {
		return animating;
	}
}
