using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	private bool grounded;
	public Transform groundCheckTransform;
	public LayerMask groundCheckLayerMask;
	public GameObject weapon;
	private float dmg;
	private float radius = 0.2f;
	private bool canMagic;
	private bool canLifeSteal;
    public int level;
    public int Exp;
	private Animator anim;
	private int currentStamina;
	private float cooldown;
    private GameObject levelDisplay;
    private GameObject expBar;


    [SerializeField]
    private Transform projectileLocation;

    [SerializeField]
    private float chargeDelayThreshold, scaleAddedPerTick, pushbackAddedPerTick, maxProjectileSize;

    [SerializeField]
    private int damageAddedPerTick;

    private float chargeShotTimer;
    private float tempScale;
    private int tempDmg;
    private float tempForce;


    void Start() {
		cooldown = 0;
        chargeShotTimer = 0.0f;
        canMagic = false;
		dmg = weapon.GetComponent<WeaponScript> ().dmg;
        anim = GetComponent<Animator>();

        levelDisplay = GameObject.Find("LevelDisplay");
        levelDisplay.GetComponentInChildren<Text>().text = "Level\n" + level;

        expBar = GameObject.Find("ExpBar");
        expBar.GetComponentInChildren<Text>().text = getExpPercentage();

	}
	void coolTheDown() {
		if (cooldown > 0)
			cooldown -= 0.1f;
		else {
			cooldown = 0;
			CancelInvoke ("coolTheDown");
		}
	}
    public void goDemonMode()
    {
        anim.SetTrigger("DemonTransformation");

        canMagic = true;
        canLifeSteal = true;


        GetComponent<PlayerControllerScript>().PlayerState = PlayerState.Demon;
        anim.SetBool("DemonForm", true);

    }

    public void goDayMode()
    {

        anim.SetTrigger("BackToNormalTransformation");

        canMagic = false;
        canLifeSteal = false;


        GetComponent<PlayerControllerScript>().PlayerState = PlayerState.Normal;

        anim.SetBool("DemonForm", false);
    }

    public bool getGrounded() {
		return grounded;
	}

    public void addExp(int value)
    {
        int v = value;

        if (v >= 10 && v < 20)
        {
            v -= 3 * (level - 1);
        }
        else if (v >= 20 && v < 50)
        {
            v -= 6 * (level - 1);
        }
        else
        {
            v -= 10 * (level - 1);
        }

        increaseExp(v);
    }

    public string getExpPercentage()
    {
        return Exp + "%";
    }

	void playerAnimator() {
		anim.SetBool("grounded", grounded);
        //Removed running set boolean you should just set it in the player move method based on it's input
	}

    void Update()
    {
        updateGroundedStatus();

        //If player can use magic
        if (canMagic == true)
        {

            //If the Right mouse button is currently held down
            if (Input.GetMouseButton(1))
            {
                if (level == 1 || level == 2)
                {
                    //Activate Timer to see if it's a normal shot or a charge one
                    chargeShotTimer = chargeShotTimer + Time.deltaTime;

                    if (!GetComponent<PlayerControllerScript>().IsChargingAShot)
                    {
                        if (chargeShotTimer > chargeDelayThreshold)
                            setIsChargedShot();
                    }
                    else
                        powerUpProjectile();

                }
            }
            //If The Right mouse button has been released
            else if (Input.GetMouseButtonUp(1))
            {
                if (level == 1 || level == 2)
                {
                    shootRapidProjectile();
                    chargeShotTimer = 0.0f;
                }

                else if (level >= 3)
                    fireBurst();
            }

        }


        currentStamina = GetComponent<PlayerControllerScript>().characterStats.Stamina;
        playerAnimator();
        if (!levelDisplay.GetComponentInChildren<Text>().text.EndsWith(level + ""))
        {
            levelDisplay.GetComponentInChildren<Text>().text = "Level\n" + level;
        }
    }

    void manageCooldown(float cd) {
		cooldown = cd;
		InvokeRepeating ("coolTheDown",0,0.1f);
	}
	void fireBurst() {
		if (cooldown == 0) {
			

			Vector3 cursorPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			bool cursorRight = false;
			if (cursorPos.x > transform.position.x)
				cursorRight = true;
			if ((cursorRight && transform.localScale.x > 0)
				|| (transform.localScale.x < 0 && !cursorRight)) {
				GameObject g = (GameObject)Instantiate (Resources.Load ("FireBurst"));
				g.transform.position = new Vector3 (cursorPos.x, transform.position.y - 1.5f, transform.position.z);
				manageCooldown (1.5f);
			}
		}
	}

    void shootFireball()
    {

        GameObject projectile;

        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        projectile = (GameObject)Instantiate(Resources.Load("Fireball"));


        if (GetComponent<PlayerControllerScript>().IsChargingAShot)
        {
            //Changes the values of the projectile
            projectile.transform.Find("FireballP").gameObject.GetComponent<ParticleSystem>().startSize = tempScale;
            projectile.GetComponent<DamageDealer>().Attack.BaseDamage = tempDmg;
            projectile.GetComponent<DamageDealer>().Attack.Force = tempForce;
        }


        projectile.transform.position = projectileLocation.position;

        float ballSpeed = 20f;

        Vector3 v = ((cursorPosition - projectile.transform.position).normalized * ballSpeed);

        v.z = 0;

        projectile.GetComponent<Rigidbody2D>().velocity = v;

        if (level == 1)
        {
            projectile.GetComponent<FireballScript>().fireBallType("normal");
        }
        else if (level == 2)
        {
            projectile.GetComponent<FireballScript>().fireBallType("exploding");
        }

        projectile.transform.GetChild(0).GetChild(0).parent = null;


        //g.transform.GetChild (0).GetChild (0).GetComponent<Rigidbody2D> ().velocity = -v;
    }


    private void setIsChargedShot()
    {
        GetComponent<PlayerControllerScript>().IsChargingAShot = true;
        tempScale = 0;
        tempDmg = 0;
        tempForce = 0;
        anim.SetTrigger("chargeShot");

    }

    private void powerUpProjectile()
    {
        if (tempScale < maxProjectileSize)
        {
            tempScale += scaleAddedPerTick;

            tempDmg += damageAddedPerTick;

            tempForce += pushbackAddedPerTick;
        }

    }

    private void shootRapidProjectile()
    {
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (canIShoot(cursorPosition))
        {
            if (GetComponent<PlayerControllerScript>().IsChargingAShot)
            {
                //The Animation is the one triggering the shot now, since the fireball triggers at a specific part of an animation
                triggerChargeShootingAnimation();
            }
            else
            {
                //The Animation is the one triggering the shot now, since the fireball triggers at a specific part of an animation
                triggerRapidShootingAnimation();
            }

        }
        else
        {
            if (GetComponent<PlayerControllerScript>().IsChargingAShot)
                anim.SetTrigger("cancelCharge");
        }


    }

    private void triggerRapidShootingAnimation()
    {
        anim.SetTrigger("shootMagic");
    }

    private void triggerChargeShootingAnimation()
    {
        anim.SetTrigger("releaseShot");
    }

    private bool canIShoot(Vector3 cursorPosition)
    {

        //If the player is trying to shoot on his right and he is facing right
        if (cursorPosition.x > transform.position.x && isFacingRight())
            return true;
        //If the player is trying to shoot on his left and he is facing left
        else if (cursorPosition.x < transform.position.x && !isFacingRight())
            return true;
        else
            //Else he is trying to shoot behind himself which isn't allowed
            return false;
    }

    private bool isFacingRight()
    {
        return GetComponent<PlayerControllerScript>().CharacterIsFacingRight();
    }


    void updateGroundedStatus() {
		
		if (Physics2D.OverlapCircle (groundCheckTransform.position, radius, groundCheckLayerMask)) {
			grounded = true;
		} else
			grounded = false;

	}

    void increaseExp(int value)
    {
        for (int i = 0; i < value; i++)
        {
            if (Exp == 100)
            {
                levelUp();
            }
            Exp += 1;
            expBar.GetComponent<Image>().fillAmount = Exp / 100.0f;
        }

        if (Exp == 100)
            levelUp();
    }

    void levelUp()
    {
        level += 1;
        Exp = 0;

        GetComponent<PlayerControllerScript>().characterStats.increaseTotalHealth(75);
        GetComponent<PlayerControllerScript>().characterStats.increaseTotalStamina(50);
        GetComponent<PlayerControllerScript>().characterStats.increaseStrength(1);
        GetComponent<PlayerControllerScript>().characterStats.increaseDefense(1);
        GetComponent<PlayerControllerScript>().characterStats.increaseCritChance(0.05f);
        GetComponent<PlayerControllerScript>().characterStats.increaseCritMultiplier(0.05f);
        GetComponent<PlayerControllerScript>().characterStats.increaseMaxForceResistence(2.5f);
        GetComponent<PlayerControllerScript>().characterStats.increaseMovementSpeed(1);

        expBar.GetComponent<Image>().fillAmount = Exp / 100.0f;
        levelDisplay.GetComponentInChildren<Text>().text = "Level\n" + level;
    }

}
