using UnityEngine;
using System.Collections;

public class FireBurstScript : MonoBehaviour {
	private Transform trigger;
	private int count;
	// Use this for initialization
	void Start () {
		count = 0;
		trigger = transform.FindChild ("TriggerBox");
		InvokeRepeating ("thriceHit",0.1f,0.2f);
		Invoke ("destroy",4f);
	}
	void destroy() {
		Debug.Log ("destroying fire burst");
		Destroy (gameObject);
	}
	void thriceHit() {
		count++;
		trigger.gameObject.GetComponent<BoxCollider2D> ().enabled = !trigger.gameObject.GetComponent<BoxCollider2D> ().enabled;
		if (count == 6)
			CancelInvoke ("thriceHit");
	}
	// Update is called once per frame
	void Update () {
	
	}
}
