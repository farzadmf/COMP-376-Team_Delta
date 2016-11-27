using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField]
    private int Exp;
	private Animator anim;
	private int currentStamina;
	private float cooldown;
	void Start() {
		cooldown = 0;
		canMagic = false;
		dmg = weapon.GetComponent<WeaponScript> ().dmg;
        anim = GetComponent<Animator>();
        GameObject.Find("LevelDisplay").GetComponentInChildren<Text>().text = "Level\n" + level;

	}
	void coolTheDown() {
		if (cooldown > 0)
			cooldown -= 0.1f;
		else {
			cooldown = 0;
			CancelInvoke ("coolTheDown");
		}
	}
	public void goDemonMode() {
		canMagic = true;
		canLifeSteal = true;
        //Replace with bool Can Use Sword in player Controller
        //weapon.SetActive (false);
        GetComponent<PlayerControllerScript>().CanUseSword = false;

    }
	public void goDayMode() {
		canMagic = false;
		canLifeSteal = false;

        //Replace with bool Can Use Sword in player Controller
        //weapon.SetActive (false);
        GetComponent<PlayerControllerScript>().CanUseSword = true;
        //weapon.SetActive (true);
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

	void Update() {
		updateGroundedStatus ();
		if (Input.GetMouseButtonDown (1)) {
			if (canMagic == true && !GetComponent<PlayerControllerScript>().IsBlocking) {
				if (level == 1 || level == 2)
					shootFireball ();
				else if (level == 3)
					fireBurst ();
			}
		}
		currentStamina = GetComponent<PlayerControllerScript> ().characterStats.Stamina;
		playerAnimator ();
        if (!GameObject.Find("LevelDisplay").GetComponentInChildren<Text>().text.EndsWith(level + ""))
        {
            GameObject.Find("LevelDisplay").GetComponentInChildren<Text>().text = "Level\n" + level;
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
	void shootFireball() {
		if (cooldown == 0) {
			Vector3 cursorPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			bool cursorRight = false;
			if (cursorPos.x > transform.position.x)
				cursorRight = true;
			if ((cursorRight && transform.localScale.x > 0)
				|| (transform.localScale.x < 0 && !cursorRight)) {
				GameObject g = (GameObject)Instantiate (Resources.Load ("Fireball"));
				g.transform.position = new Vector3 (transform.position.x + 1, transform.position.y, g.transform.position.z);
			
				float ballSpeed = 20f;
				Vector3 v = ((cursorPos - g.transform.position).normalized * ballSpeed);
				v.z = 0;
				if (transform.localScale.x < 0) {
					g.transform.position = new Vector3 (transform.position.x - 1, transform.position.y, transform.position.z);
				}

				g.GetComponent<Rigidbody2D> ().velocity = v;
				if (level == 1) {
					g.GetComponent<FireballScript> ().fireBallType ("normal");
					manageCooldown (0.5f);
				} else if (level == 2) {
					manageCooldown (0.7f);
					g.GetComponent<FireballScript> ().fireBallType ("exploding");
				}
				g.transform.GetChild (0).GetChild (0).parent = null;
			}
		}
		//g.transform.GetChild (0).GetChild (0).GetComponent<Rigidbody2D> ().velocity = -v;
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
            GameObject.Find("ExpBar").GetComponent<Image>().fillAmount = Exp / 100.0f;
        }

        if (Exp == 100)
            levelUp();
    }

    void levelUp()
    {
        level += 1;
        Exp = 0;

        GetComponent<PlayerControllerScript>().characterStats.increaseTotalHealth(200);
        GetComponent<PlayerControllerScript>().characterStats.increaseTotalStamina(50);
        GetComponent<PlayerControllerScript>().characterStats.increaseStrength(1);
        GetComponent<PlayerControllerScript>().characterStats.increaseDefense(1);
        GetComponent<PlayerControllerScript>().characterStats.increaseCritChance(0.05f);
        GetComponent<PlayerControllerScript>().characterStats.increaseCritMultiplier(0.05f);
        GetComponent<PlayerControllerScript>().characterStats.increaseMaxForceResistence(2.5f);
        GetComponent<PlayerControllerScript>().characterStats.increaseMovementSpeed(1);

        GameObject.Find("ExpBar").GetComponent<Image>().fillAmount = Exp / 100.0f;
        GameObject.Find("LevelDisplay").GetComponentInChildren<Text>().text = "Level\n" + level;
    }

}
