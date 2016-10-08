using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Character : MonoBehaviour {


    public Animator ThisAnimator { get; private set; }

    public bool IsTakingDamage { get;  set; }

    [SerializeField]
    protected int health;

    [SerializeField]
    protected float movementSpeed;

    [SerializeField]
    private List<string> damageSources;
   
    //If not faceing right then facing left
    protected bool isFacingRight;

    // Use this for initialization
    public virtual void Start () {

        //By default a character will start by facing right
        isFacingRight = true;
        ThisAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void ChangeDirection()
    {
        //Change facing direction to the opposite of what it currently is
        isFacingRight = !isFacingRight;

        //Rotate 180 to go towards other direction
        transform.Rotate(new Vector3(transform.rotation.x, 180, transform.rotation.z));
     
         
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
		if (gameObject.tag == "Player" && IsDead ())
			Destroy (gameObject);
		else {

			if (!IsDead ()) {
				if (ThisAnimator != null)
					ThisAnimator.SetTrigger ("damage");
			} else {
			
				if (ThisAnimator != null)
					ThisAnimator.SetTrigger ("death");
				else
					Destroy (gameObject);
			}
		}
    }
	public virtual void OnCollisionEnter2D(Collision2D other) {
		if(damageSources.Contains(other.gameObject.tag))
		{
			TakeDamage(10);
			//other.gameObject.GetComponent<BasicProjectile>().GetDamage()
		}
	}
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
       
        if(damageSources.Contains(other.tag))
        {
            TakeDamage(10);
			if (other.tag == "Projectile")
				Destroy (other.gameObject);
            //other.gameObject.GetComponent<BasicProjectile>().GetDamage()
        }
    }
}
