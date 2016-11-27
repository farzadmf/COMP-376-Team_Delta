using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CannonScript : MonoBehaviour {



	public GameObject tun;
	public float velocityX;
	public float velocityY;
	public AudioClip shootSound;
	public float rotationOfBullet;
	public float time;
	public Sprite cannonBeforeFiring;
	public Sprite cannonAfterFiring;
	public bool gravity;
	private Transform playerTrans;
	private SpriteRenderer currentSpriteRenderer;
	public ParticleSystem beam;
	public float timeOffForBeam;
	private bool on;
	public bool SmallObjectShooter;
	public int shape;
	public bool active;
	public bool fireSpitter;
	private AudioSource audioSource1;
	void activateEvent(bool a) {
		active = !a;
}
	void addBullet() {
		if (active) {
			currentSpriteRenderer.sprite = cannonAfterFiring;


			GameObject bullet;
			if (SmallObjectShooter) {
				if (shape == 1)
					bullet = (GameObject)Instantiate (Resources.Load ("Square"));
				else if (shape == 2)
					bullet = (GameObject)Instantiate (Resources.Load ("Triangle"));
				else 
					bullet = (GameObject)Instantiate (Resources.Load ("Circle"));
			} else {

				bullet = (GameObject)Instantiate (Resources.Load ("Bullet 1"));
			}
			//5
			bullet.transform.position = tun.transform.position;

			/*
			if (gravity) {
				bullet.transform.FindChild("Bullet").GetComponent<Rigidbody2D> ().gravityScale = 0.5f;
			} else {
				bullet.transform.FindChild("Bullet").GetComponent<Rigidbody2D> ().gravityScale = 0;
			}*/
			Vector2 newVelocity = bullet.transform.FindChild("Bullet").GetComponent<Rigidbody2D> ().velocity;
			newVelocity.y = velocityY;
			newVelocity.x = velocityX;
			bullet.transform.FindChild ("Fireball").GetComponent<ParticleSystem> ().startSpeed = -newVelocity.y;
			bullet.transform.FindChild ("Fireball2").GetComponent<ParticleSystem> ().startSpeed = -newVelocity.y;
			bullet.transform.FindChild ("Fireball3").GetComponent<ParticleSystem> ().startSpeed = -newVelocity.y;
			bullet.transform.FindChild("Bullet").GetComponent<Rigidbody2D> ().velocity = newVelocity;

	
			playerSoundNearPlayer ();
		}
	}
	void resetSprite() {
		if (active)
		currentSpriteRenderer.sprite = cannonBeforeFiring;
	}
	void Start () {
		playerTrans =  GameObject.FindWithTag ("Player").transform;
		audioSource1 = gameObject.AddComponent<AudioSource> ();
		audioSource1.clip = shootSound;
		currentSpriteRenderer = GetComponent<SpriteRenderer> ();
		if (!fireSpitter) {

			InvokeRepeating ("addBullet", time, time);
			InvokeRepeating ("resetSprite", time * 1.2f, time);
		} else if (fireSpitter) {
			gravity = false;
			InvokeRepeating("spitFire",time, time);
			InvokeRepeating ("resetSprite", time * 1.2f, time);
		}
	}

	void spitFire() {
		if (active) {
			currentSpriteRenderer.sprite = cannonAfterFiring;
			
			
			GameObject bullet = (GameObject)Instantiate (Resources.Load ("FireBullet"));
		
				//bullet = ;

			//5
			bullet.transform.position = tun.transform.position;
			
			
			if (gravity) {
				bullet.GetComponent<Rigidbody2D> ().gravityScale = 0.5f;
			} else {
				bullet.GetComponent<Rigidbody2D> ().gravityScale = 0;
			}
			Vector2 newVelocity = bullet.GetComponent<Rigidbody2D> ().velocity;
			newVelocity.y = velocityY;
			newVelocity.x = velocityX;
			
			bullet.GetComponent<Rigidbody2D> ().velocity = newVelocity;
			
			
			bullet.transform.rotation = Quaternion.Euler (Vector3.forward * rotationOfBullet);
			/*
			if (distanceX <= 10 && distanceY <= 10) {
				float volume = 1 - distanceX / 10;
				audioo.volume = volume;

				audioo.Play();
			}
			*/
		}
	}
	void BeamOnOff() {
		if (on == true) {
			ParticleSystem ps = beam;
			var em = ps.emission;
			em.enabled = false;
			beam = ps;
			//beam.enableEmission = false;
			on = false;
		}
		else {
			ParticleSystem ps = beam;
			var em = ps.emission;
			em.enabled = true;
			beam = ps;
			//beam.emission.enabled = true;
			on = true;
		}
	}
	
	void FixedUpdate() {

	}

	void OnCollisionEnter2D(Collision2D c) {

		if (c.gameObject.name == "Spike") {
			StartCoroutine(delayedStopOfEffect());
		}
	}
	IEnumerator delayedStopOfEffect() {
		
		yield return new WaitForSeconds (0.1f);
		Destroy (this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D c) {
		if (c.tag == "Player") {
			GetComponent<CannonScript> ().enabled = true;
			active = true;
		}
	}

	void playerSoundNearPlayer() {
		if (playerTrans && !audioSource1.isPlaying) {
			float distanceZ = Mathf.Abs (transform.position.x - playerTrans.position.x);
			if (distanceZ <= 10) {

				float volume = 1 - distanceZ / 10;
				audioSource1.volume = volume;
				audioSource1.Play ();
			} else {
				audioSource1.Stop ();
			}
		}
	}
}
