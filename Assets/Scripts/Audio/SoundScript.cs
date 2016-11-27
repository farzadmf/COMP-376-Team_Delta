using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour {
	private Animator anim;
	private AudioSource audio1;
	private AudioSource audio2;
	// Use this for initialization
	void Start () {
		audio1 = gameObject.AddComponent<AudioSource> ();
		audio2 = gameObject.AddComponent<AudioSource> ();

		if (gameObject.tag == "Enemy" || gameObject.tag == "Player") {
			anim = GetComponent<Animator> ();
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
