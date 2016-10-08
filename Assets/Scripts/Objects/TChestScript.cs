using UnityEngine;
using System.Collections;

public class TChestScript : MonoBehaviour {
	public GameObject loot;
	private bool droppedOnce;
	// Use this for initialization
	void Start () {
		droppedOnce = false;
	}

	void OnTriggerEnter2D(Collider2D c) {
		
	}

	public void activatedChest() {
		int random = Random.Range (0, 1);
		if (random == 0) {
			if (droppedOnce == false) {
				dropLoot ();
				droppedOnce = true;
			}
		} else
			goMimic ();
	}

	void dropLoot() {
		
		loot = Instantiate (loot);
		loot.transform.position = transform.position;
		loot.AddComponent<Rigidbody2D> ();
		Rigidbody2D body = loot.GetComponent<Rigidbody2D> ();
		body.velocity = new Vector2 (-2f,6f);
		body.gravityScale = 1;
		body.freezeRotation = true;
		loot.AddComponent<BoxCollider2D> ();
		if (loot.GetComponent<WeaponScript> ()) {
			loot.GetComponent<WeaponScript> ().onGround = true;
		}
	}
	void goMimic() {
	}
	// Update is called once per frame
	void Update () {
	
	}
}
