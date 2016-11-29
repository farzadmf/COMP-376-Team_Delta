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
			saveGame ();

		}
	}
	void saveGame() {
		SaveLoadScript.SaveGame (player.GetComponent<PlayerControllerScript> ().characterStats,
			player.GetComponent<PlayerScript>().level, player.GetComponent<PlayerScript>().Exp);
	}
	void Start () {
		player = GameObject.Find ("Player");
	}

}
