using UnityEngine;
using System.Collections;

public class BatScript : Character {
	public Transform target;
	private float moveSpeed = 1.5f; //move speed 
	//private float rotationSpeed = 0.5f; //speed of turning
	private bool inPursue;
	private bool batHung;
	private Animator anim;
	private Vector3 originalPosition;
	//public AudioClip flyingSound;
	private bool dead;
	private Quaternion originalRotation;
	private BoxCollider2D boxCol;
	public AudioSource audio1;
	public AudioSource audio2;
	private bool deaad;
//	private CircleCollider2D circle;

	// Use this for initialization
	public override void OnCollisionEnter2D(Collision2D c) {
		base.OnCollisionEnter2D (c);
		if (c.gameObject.tag == "Bullet") {
			killMonster ();
			Physics2D.GetIgnoreCollision (c.collider, GetComponent<BoxCollider2D> ());

		}
	}
	void killMonster() {
		/*
		if (!audio2.isPlaying)
			audio2.Play ();
			*/
		dead = true;
		//specialWalkingEffect.enableEmission = false;
		GetComponent<Rigidbody2D> ().gravityScale = 0;
		GetComponent<BoxCollider2D> ().enabled = false;
		anim.SetBool ("dead", dead);
		StartCoroutine (DestroyMonster());
		
	}
	
	IEnumerator DestroyMonster() {
	

		
		yield return new WaitForSeconds (1f);
		
		Destroy (transform.parent.gameObject);
	}

	void activateParticleCollisionEvent(bool a) {
		killMonster ();
	}
	public override void Start () {
		base.Start ();
		//circle = GetComponent<CircleCollider2D> ();
		anim = GetComponent<Animator> ();
		target = GameObject.FindWithTag ("Player").transform;
		batHung = true;
		originalPosition = transform.position;
		originalRotation = transform.rotation;
		boxCol = GetComponent<BoxCollider2D> ();
		//audio1.volume = 0f;
		//circle.enabled = false;
	}
	public void trigEnter() {
		inPursue = true;
		batHung = false;
		anim.Play (Animator.StringToHash ("Base Layer.BatFly"));
	}
	public void trigExit() {
		inPursue = false;
		returnToOriginalPosition();
	}
	public override void OnTriggerEnter2D(Collider2D c) {
		base.OnTriggerEnter2D (c);
	}
	void OnTriggerExit2D(Collider2D c) {

	}
	void returnToOriginalPosition() {
		transform.LookAt(originalPosition);
		transform.Rotate(new Vector3(0,90,0),Space.Self);
		transform.Translate(new Vector3(-moveSpeed* Time.deltaTime,0,0) );
	}
	void pursuePlayer() {
		transform.LookAt(target.position);
		transform.Rotate(new Vector3(0,90,0),Space.Self);//correcting the original rotation
		
		
		//move towards the player
		//if (Vector3.Distance(transform.position,target.position)>1f){//move if distance from target is greater than 1
			transform.Translate(new Vector3(-moveSpeed* Time.deltaTime,0,0) );
		/*
		float distanceX = Mathf.Abs (transform.position.x - target.position.x);
		float distanceY = Mathf.Abs (transform.position.y - target.position.y);
		if (distanceX <= 15 && distanceY <= 15) {
			float volume = 1 - distanceX / 15;
			if (!audio1.isPlaying) {

				audio1.volume = volume;
				audio1.Play();

			}
		}
		*/
	}
	void checkAndSetHung() {
		if (Mathf.Abs (originalPosition.x - transform.position.x) < 0.05 &&
			Mathf.Abs (originalPosition.y - transform.position.y) < 0.05) {
			if (!inPursue) {
				batHung = true;
				if (transform.position != originalPosition ||transform.rotation != originalRotation) {
					transform.rotation = originalRotation;
					transform.position = originalPosition;
				}
			}

		} 
		/*else {
			batHung = false;

		}
*/

	}
	void FixedUpdate () {

		checkAndSetHung ();
		if (batHung)
			anim.SetBool("batHung",batHung);
		if (inPursue) {
			anim.SetBool("batInPursue",inPursue);
			pursuePlayer ();
		}
		else if (!inPursue && !batHung) {

			returnToOriginalPosition();
		}

		if (anim.GetBool ("death") == true && deaad == false) {
			deaad = true;
			StartCoroutine (DestroyMonster ());
		}

	}
}
