using UnityEngine;
using System.Collections;

public class BatVisionScript : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D c) {
		if (c.gameObject.tag == "Player") {
			transform.parent.GetChild(0).GetComponent<BatScript> ().trigEnter ();
		}
	}
	void OnTriggerExit2D(Collider2D c) {
		if (c.gameObject.tag == "Player")
			transform.parent.GetChild(0).GetComponent<BatScript> ().trigExit ();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
