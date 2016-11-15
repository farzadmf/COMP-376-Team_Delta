using UnityEngine;
using System.Collections;

public class SunTriggerScript : MonoBehaviour {
	public bool sunOn;
	private GameObject sun;
	// Use this for initialization
	void Start () {
		sun = GameObject.Find ("Sun");
	}
	void OnTriggerEnter2D(Collider2D c) {
		if (!sunOn) {
			if (c.tag == "Player") {
				
			}
		} else {
			if (c.tag == "Player") {
				
			}
		}

	}

	// Update is called once per frame
	void Update () {
	
	}
}
