using UnityEngine;
using System.Collections;

public class CheckpointScript : MonoBehaviour {

	private GameObject player;
	public bool activated;
	public ParticleSystem ray;
	public int numberTag;
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Player" && !activated) {
			activated = true;
			ParticleSystem ps = ray;
			var em = ps.emission;
			em.enabled = true;
			ray = ps;

			//ray.enableEmission = true;
			//Debug.Log ("ray: " + ray);
			//SaveLoadScript.tempCoins = mouse.getCoins ();
			//SaveLoadScript.SaveTempCoins ();

		}
	}
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
