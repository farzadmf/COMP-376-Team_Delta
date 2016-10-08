using UnityEngine;
using System.Collections;

public class FireballScript : MonoBehaviour {
	public string type; // "normal" or "exploding"
	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionEnter2D(Collision2D c) {
		if (c.gameObject.tag == "Enemy") {
			
		}
		if (type == "exploding") {
			GameObject g = (GameObject)Instantiate (Resources.Load ("Boom"));
			g.transform.position = transform.position;
		}
		Destroy (gameObject);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
