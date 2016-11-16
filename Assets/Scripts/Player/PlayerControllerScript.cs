using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class PlayerControllerScript : Character
{
    private float movex = 0f; // 4
    private float movey = 0f;
    public float jumpHeight;
    private Rigidbody2D rigidBody;
    private bool canDoubleJump;
    public CheckpointScript[] checkpoints;
    public string levelString;
    public BoxCollider2D rightCollider;
    public BoxCollider2D leftCollider;
    public BoxCollider2D bodyCollider;
    private bool canMoveX;
    private LayerMask groundCheckLayerMask;


    //State Variables
    public bool IsAttacking { get; set; }
    public bool CanUseSword { get; set; }
    public bool IsDodging { get; set; }
    public bool IsBlocking { get; set; }
    
  

    //Attack variables
    [SerializeField]
    private float delayForCombo;
    private bool firstAttack;

    private float nextAttackTimer;

    //Constants For Animations
    private const int NUM_ATTACKS = 3;
    private string[] ATTACKS = new string[] { "attack1", "attack2", "attack3" };

    private int attackCounter;

    //Dodge variables
    [SerializeField]
    private float dodgeForce;

    [SerializeField]
    private float blockSpeed;

    [SerializeField]
    private GameObject  shield;
	public int staminaRegen;
	public int hpRegen;

    // Use this for initialization
    public override void Start() {
        SaveLoadScript.LoadGame();
        CharacterStats stats = SaveLoadScript.playerStats;
        // check if game was saved at least once, otherwise we put the player stats to null
        if (SaveLoadScript.saved == true) {
            base.characterStats = stats;
        }

        canMoveX = true;
        rigidBody = GetComponent<Rigidbody2D>();
        //Call the Parent's class start method
        base.Start();
        groundCheckLayerMask = LayerMask.GetMask("Ground", "UndergroundGround");

        //initial set of state variables
        IsAttacking = false;
        IsDodging = false;
        CanUseSword = true;
        attackCounter = 0;
        firstAttack = true;
        setIsBlocking(false);
		InvokeRepeating ("staminaRegeneration", 0, 0.1f);
		InvokeRepeating ("hpRegeneration", 0, 0.1f);
    }

    protected override void Update()
    {
        base.Update();

        //Player can't move,jump or attack if he is taking damage
        if (!base.IsTakingDamage)
        {
            //Player can't move while attacking
            if (!IsAttacking && !IsDodging)
            {
                move();
                block();
                if (!IsBlocking)
                {
                    jump();
                    dodge();
                    if (CanUseSword)
                        attack();
                }

            }
        }
     

        checkMoveX ();
		fixTextOrientation ();
		checkDeathAndRestart ();

    }

    //Added a fixed update, Physics calls should be in a fixed update
     void FixedUpdate()
    {
    }

    void checkDeathAndRestart() {
		bool dead = false;
		if (base.characterStats.Health <= 0)
			dead = true;
		if (dead) {
			SceneManager.LoadScene (getWholeLevelSceneString ());
		}
	}
	void attack() {
		if (Input.GetMouseButtonDown (0)) { // left click

            ThisAnimator.SetTrigger(getNextAttack());
            IsAttacking = true;
        }
	}

    void dodge()
    {
        if(GetComponent<PlayerScript>().getGrounded() == true)
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                ThisAnimator.SetTrigger("dodge");
				base.characterStats.decreaseStamina (10);
                rigidBody.velocity = new Vector2(0,0);
                SetFacingDirection();
                if (isFacingRight)
                    rigidBody.AddForce(new Vector2(dodgeForce, 0));
                else
                    rigidBody.AddForce(new Vector2(-dodgeForce, 0));
                IsDodging = true;
            }
        }
    }
	void staminaRegeneration() {
		
		if (base.characterStats.Stamina < base.characterStats.TotalStamina) {
			GetComponent<PlayerControllerScript> ().characterStats.increaseStamina (staminaRegen);
		}
	}
	void hpRegeneration() {

		if (base.characterStats.Health < base.characterStats.TotalHealth) {
			GetComponent<PlayerControllerScript> ().characterStats.increaseHealth (hpRegen);
		}
	}
    void block()
    {
        if (GetComponent<PlayerScript>().getGrounded() == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                ThisAnimator.SetTrigger("initialBlock");
                setIsBlocking(true);
                shield.GetComponent<BoxCollider2D>().enabled = true;
            }
            if(Input.GetKeyUp(KeyCode.LeftShift) || !Input.GetKey(KeyCode.LeftShift))
            {
                setIsBlocking(false);
                shield.GetComponent<BoxCollider2D>().enabled = false;
            }
               
        }
    }


    void jump() {
		if (Input.GetKeyDown("space")) {
			if (GetComponent<PlayerScript> ().getGrounded () == true) {
				rigidBody.velocity = new Vector2 (rigidBody.velocity.x,0);
				rigidBody.AddForce (new Vector2 (0, jumpHeight));
				canDoubleJump = true;
                doubleJumpState();
            } else {
				if (canDoubleJump == true) {
					canDoubleJump = false;
                    doubleJumpState();
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x,0);
					rigidBody.AddForce (new Vector2 (0, jumpHeight));
				}
			}

		}
	}

	void move() {

		movex = Input.GetAxis ("Horizontal");
        //movey = Input.GetAxis ("Vertical"); we'll use this later for ladders

        //Set run animation based on input
        ThisAnimator.SetFloat("speed", Mathf.Abs(movex));

        //Tell animator the Y velocity
        ThisAnimator.SetFloat("ySpeed", rigidBody.velocity.y);

        if (canMoveX)
			rigidBody.velocity = new Vector2 (IsBlocking? movex * blockSpeed : movex * characterStats.MovementSpeed,rigidBody.velocity.y);

		float x = transform.localScale.x;
		if (movex > 0 && x < 0)
			transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		else if (movex < 0 && x > 0)
			transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            
	}

	void fixTextOrientation() {
		GameObject text = transform.FindChild ("Partner").FindChild("Canvas").FindChild("Text").gameObject;
		Quaternion rot = text.transform.rotation;

		if (transform.localScale.x > 0) {
			rot.eulerAngles = new Vector3 (0, 0, 0);

		} else {
			rot.eulerAngles = new Vector3 (0, -180, 0);
		}
		text.transform.localRotation = rot;
        
	}
	string getWholeLevelSceneString() {
		int latestCheckpoint = 0;
		for (int i = 0; i < checkpoints.Length; ++i) {
			int t = checkpoints[i].numberTag;
			if (t > latestCheckpoint && checkpoints[i].activated == true) {
				latestCheckpoint = t;
			}

		}
		string s = levelString + latestCheckpoint;
		return s;
	}
	void smoothTerrain(Collision2D coll) {
		for ( int i = 0; i < coll.contacts.Length; ++i) {
			ContactPoint2D p = coll.contacts[i];

			Vector3 pos = transform.position;
			if (p.normal.x <= -0.9f && p.normal.x >= -1.0f && p.normal.y == 0f)
				pos.x += 0.08f;
			else if (p.normal.x <= 1.0f && p.normal.x >= 0.9f && p.normal.y == 0f)
				pos.x -= 0.08f;
			transform.position = pos;
		}
	}
	public override void OnTriggerEnter2D(Collider2D c) {

        if (c.gameObject.tag == "DeathZone")
            characterStats.Health = 0;
        else
        {
            if (bodyCollider.IsTouching(c))
            {
                base.OnTriggerEnter2D(c);
            }

            if (c.gameObject.tag == "Ground" && (rightCollider.IsTouching(c) || (leftCollider.IsTouching(c))))
            {
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
                canMoveX = false;
            }
        }
     
	}

	void checkMoveX() {
		if (!rightCollider.IsTouchingLayers(groundCheckLayerMask) && 
			!leftCollider.IsTouchingLayers(groundCheckLayerMask)) {
			canMoveX = true;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		
		if (coll.gameObject.tag == "Ground")
			smoothTerrain (coll);
	}

    //Tell the animator the current state of the double jump
    private void doubleJumpState()
    {
        //If he can double jump then tell animator that he currently isn't double jumping
        ThisAnimator.SetBool("inDoubleJump", !canDoubleJump);
    }

    //Gets next attack string for animator based on if the player is fast enough for a combo or it returns to first attack
    private string getNextAttack()
    {
        if (firstAttack)
        {

            nextAttackTimer = delayForCombo + Time.time;

            firstAttack = false;

            return ATTACKS[attackCounter];
        }
        else
        {

            if (nextAttackTimer > Time.time)
            {
                attackCounter++;

                if (attackCounter >= ATTACKS.Length)
                    attackCounter = 0;
            }
            else
                attackCounter = 0;


            nextAttackTimer = delayForCombo + Time.time;
            return ATTACKS[attackCounter];
        }
   

    }

    private void setIsBlocking(bool value)
    {
        IsBlocking = value;
        ThisAnimator.SetBool("block", IsBlocking);
    }

}
