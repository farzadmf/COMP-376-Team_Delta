using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class BulletScript : MonoBehaviour {

	private GameObject mouse;
	public AudioClip bulletDestroySound;
	public int kindOfBullet; // 1 normal, 2 fire
	ParticleSystem p;
	private AudioSource audioo;
	private Rigidbody2D body;
	void OnCollisionEnter2D(Collision2D coll) {
	//	if (kindOfBullet == 1)
			//explodeParticle ();
		//body.velocity = Vector2.zero;
		StartCoroutine(destroy ());
		//Debug.Log (mouse.transform.position.x);
		float distanceX = Mathf.Abs (transform.position.x - mouse.transform.position.x);
		float distanceY = Mathf.Abs (transform.position.y - mouse.transform.position.y);
		/*
		if (distanceX <= DataManagerScript.distanceForSFX && distanceY <= DataManagerScript.distanceForSFX) {
			float volume = 1 - distanceX/DataManagerScript.distanceForSFX;
			audioo.volume = volume;

			audioo.Play();
		}*/

	}
	IEnumerator destroy() {
		yield return new WaitForSeconds (0.01f);

		Transform t = transform.parent.FindChild ("Fireball");
		t.gameObject.SetActive(false);

		GetComponent<CircleCollider2D> ().enabled = false;
		Transform fireSpitter;
		if (transform.childCount > 1)
			fireSpitter = transform.GetChild (1);
		else {
			fireSpitter = null;
			//return false;
		}

		if (fireSpitter) {

			fireSpitter.gameObject.SetActive(true);
		}

		yield return new WaitForSeconds (2f);
		//nothing ();
		Destroy (transform.parent.gameObject);

	}
	void terminate() {
		if (this.gameObject)
		Destroy (this.gameObject);
	}
	void explodeParticle() {
#if UNITY_EDITOR
		SerializedObject so = new SerializedObject(GetComponent<ParticleSystem>());
		p.startLifetime = 0.1f;
		so.FindProperty ("ShapeModule.randomDirection").boolValue = true;
		so.ApplyModifiedProperties();
#endif
	}
	// Use this for initialization
	void Start () {
		mouse = GameObject.FindWithTag ("Player");
		body = GetComponent<Rigidbody2D> ();
		Invoke ("terminate", 4f);
		/*
		audioo = GetComponent<AudioSource> ();
		audioo.clip = bulletDestroySound;
		*/
		p = GetComponent<ParticleSystem> ();

	}
	
	// Update is called once per frame
	void Update () {

	}
}
