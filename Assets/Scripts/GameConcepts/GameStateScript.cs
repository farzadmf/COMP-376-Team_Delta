using UnityEngine;
using System.Collections;

public class GameStateScript : MonoBehaviour {
	private GameObject sun;
	private GameObject enemies;
	private GameObject effects;
	private GameObject player;
	private bool isDay;
	private int currentTime;
	private int nightStart;
	private bool transitionStart;
	private string toState; // to day or night
	private int levelMultiplier;
	[SerializeField]
	public CharacterStats meleeGoblinNightStats;
	[SerializeField]
	public CharacterStats rangedGoblinNightStats;
	[SerializeField]
	public CharacterStats batNightStats;
	private CharacterStats meleeGoblinDayStats;
	private CharacterStats rangedGoblinDayStats;
	private CharacterStats batDayStats;
	public GameObject nightTimeEnemies;
	private Light sfxNightTime;
	private float rangeChange;
	private bool paused;
	// Use this for initialization
	void Start () {
		rangeChange = 25f;
		sfxNightTime = transform.FindChild ("SFXNightTime").GetComponent<Light>();
		levelMultiplier = 1;
		transitionStart = false;
		sun = GameObject.Find ("Sun");
		enemies = GameObject.Find ("Enemies");
		effects = GameObject.Find ("Effects");
		player = GameObject.Find ("Player");
		toState = "Night";
		getDayStats ();

	}
	void getDayStats() {
		for (int i = 0; i < enemies.transform.childCount; ++i) {
			GameObject enemyParent = enemies.transform.GetChild (i).gameObject;
			if (!enemyParent.name.Contains ("Night")) {
				GameObject enemy = enemyParent.transform.FindChild ("Enemy").gameObject;
				if (enemyParent.name.Contains ("GoblinMelee")) {
					meleeGoblinDayStats = enemy.GetComponent<Enemy> ().characterStats; 
				} else if (enemyParent.name.Contains ("GoblinRanged")) {
					rangedGoblinDayStats = enemy.GetComponent<Enemy> ().characterStats; 
				} else if (enemyParent.name.Contains ("Bat")) {
					batDayStats = enemy.GetComponent<BatScript> ().characterStats; 
				}
			}
		}
	}
	// Update is called once per frame
	void Update () {
		checkUserInput ();
		updateDayStatus ();
		checkTransitionState ();
		if (transitionStart == true)
			updateGameState ();
	}
	void checkUserInput() {
		if (Input.GetKeyDown("p")) {
			pauseGame();
		}
	}
	void changePlayer() {
		if (toState == "Night") {
			player.GetComponent<PlayerScript> ().goDemonMode ();
		} else if (toState == "Day") {
			player.GetComponent<PlayerScript> ().goDayMode ();
		}
	}

	void addSpecialEnemies() {
		if (toState == "Night") {
			nightTimeEnemies.SetActive (true);

		} else if (toState == "Day") {
			nightTimeEnemies.SetActive (false);
		}
	}
	void specialFX() {
		
		InvokeRepeating ("lightUp",0,0.01f);
		InvokeRepeating ("lightDown",0.5f,0.01f);
		Invoke ("cancelLightUp", 0.5f);
		Invoke ("cancelLightDown",1);
	}
	void lightUp() {
		sfxNightTime.range += rangeChange;
	}
	void lightDown() {
		sfxNightTime.range -= rangeChange;
	}
	void cancelLightUp() {
		CancelInvoke ("lightUp");
	}
	void cancelLightDown() {
		CancelInvoke ("lightDown");
	}
	void updateGameState() {
		
		changePlayer ();
		changeEnemies ();
		addSpecialEnemies ();
		specialFX ();
		transitionStart = false;
	}
	void checkTransitionState() {
		if (currentTime == 0) {
			toState = "Day";
			transitionStart = true;
		} else if (currentTime == nightStart) {
			toState = "Night";
			transitionStart = true;
		} else
			transitionStart = false;
	}
	void updateDayStatus() {
		isDay = sun.transform.GetChild(0).GetComponent<SunScript> ().isDay;
		currentTime = sun.transform.GetChild(0).GetComponent<SunScript> ().currentTime;
		nightStart = sun.transform.GetChild(0).GetComponent<SunScript> ().nightStart;
	}

	void changeEnemies() {
		if (toState == "Night") {
			for (int i = 0; i < enemies.transform.childCount; ++i) {
				GameObject enemyParent = enemies.transform.GetChild (i).gameObject;
				if (!enemyParent.name.Contains ("Night")) {
					GameObject enemy = enemyParent.transform.FindChild ("Enemy").gameObject;
					if (enemyParent.name.Contains ("GoblinMelee")) {
						Color newColor = new Color (85f / 255f, 240f / 255f, 100f / 255f);
						enemy.GetComponent<SpriteRenderer> ().color = newColor;
						CharacterStats stats = enemy.GetComponent<Enemy> ().characterStats;
						stats.CritChance = 0.25f * levelMultiplier;
						stats.CritMultiplier = 1.5f * levelMultiplier;
						stats.TotalHealth *= (int)(2f * levelMultiplier);
						stats.Health *= (int)(2f * levelMultiplier);
						stats.Defense = (5 + levelMultiplier);
						stats.Strength *= (3 + levelMultiplier);
						stats.MovementSpeed = 8;
						enemy.GetComponent<Character> ().characterStats = stats;
					} else if (enemyParent.name.Contains ("GoblinRanged")) {
						Color newColor = new Color (85f / 255f, 240f / 255f, 100f / 255f);
						enemy.GetComponent<SpriteRenderer> ().color = newColor;
						CharacterStats stats = enemy.GetComponent<Enemy> ().characterStats;
						//Debug.Log ("color: " + enemy.GetComponent<SpriteRenderer> ().color);
						stats.CritChance = 0.25f * levelMultiplier;
						stats.CritMultiplier = 1.5f * levelMultiplier;
						stats.TotalHealth *= (int)(2f * levelMultiplier);
						stats.Health *= (int)(2f * levelMultiplier);
						stats.Defense = (5 + levelMultiplier);
						stats.Strength *= (3 + levelMultiplier);
						stats.MovementSpeed = 8;
						enemy.GetComponent<Character> ().characterStats = stats;
					} else if (enemyParent.name.Contains ("BatParent")) {
						CharacterStats stats = enemy.GetComponent<BatScript> ().characterStats;
						stats.CritChance = 0.25f * levelMultiplier;
						stats.CritMultiplier = 1.5f * levelMultiplier;
						stats.TotalHealth *= (int)(2f * levelMultiplier);
						stats.Health *= (int)(2f * levelMultiplier);
						stats.Defense = (5 + levelMultiplier);
						stats.Strength *= (3 + levelMultiplier);
						stats.MovementSpeed = 8;
						enemy.GetComponent<Character> ().characterStats = stats;
					}
				}
			}
		} else if (toState == "Day") {
			for (int i = 0; i < enemies.transform.childCount; ++i) {
				GameObject enemyParent = enemies.transform.GetChild (i).gameObject;
				if (!enemyParent.name.Contains ("Night")) {
					GameObject enemy = enemyParent.transform.FindChild ("Enemy").gameObject;
					if (enemyParent.name.Contains ("GoblinMelee")) {
						CharacterStats stats = enemy.GetComponent<Enemy> ().characterStats;
						Color newColor = new Color (1, 1, 1);
						enemy.GetComponent<SpriteRenderer> ().color = newColor;
						stats.CritChance = 0;
						stats.CritMultiplier = 0;
						stats.TotalHealth /= (int)2f * levelMultiplier;
						stats.Health /= (int)2f * levelMultiplier;
						stats.Defense = 0;
						stats.Strength /= (3 + levelMultiplier);
						stats.MovementSpeed = 6;
						enemy.GetComponent<Character> ().characterStats = stats;
					} else if (enemyParent.name.Contains ("GoblinRanged")) {
						CharacterStats stats = enemy.GetComponent<Enemy> ().characterStats;
						Color newColor = new Color (1, 1, 1);
						enemy.GetComponent<SpriteRenderer> ().color = newColor;
						stats.CritChance = 0;
						stats.CritMultiplier = 0;
						stats.TotalHealth /= (int)2f * levelMultiplier;
						stats.Health /= (int)2f * levelMultiplier;
						stats.Defense = 0;
						stats.Strength /= (3 + levelMultiplier);
						stats.MovementSpeed = 6;
						enemy.GetComponent<Character> ().characterStats = stats;
					} else if (enemyParent.name.Contains ("BatParent")) {
						CharacterStats stats = enemy.GetComponent<BatScript> ().characterStats;
						stats.CritChance = 0;
						stats.CritMultiplier = 0;
						stats.TotalHealth /= (int)2f * levelMultiplier;
						stats.Health /= (int)2f * levelMultiplier;
						stats.Defense = 0;
						stats.Strength /= (3 + levelMultiplier);
						stats.MovementSpeed = 6;
						enemy.GetComponent<Character> ().characterStats = stats;
					}
				}
			}
		}

	}
	void OnGUI() {
		DisplayConfirmationButton ();
	}

	void pauseGame() {
		if (!paused)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
		paused = !paused;
	}
	void resumeGame() {
		Time.timeScale = 1;
	}

	void DisplayConfirmationButton() {
		if (paused) {
			Rect buttonRect = new Rect (Screen.width * 0.15f, Screen.height * 0.45f, 
				Screen.width * 0.30f, Screen.height * 0.1f);
			Rect buttonRect2 = buttonRect;
			buttonRect2.x = buttonRect.x + buttonRect.width + 20;

			if (GUI.Button (buttonRect, "Yes")) {
				resumeGame();
				//SceneManager.LoadScene("EleChoiceScene");
			} else if (GUI.Button (buttonRect2, "No")) {

				resumeGame();
				paused = false;
			}
		}
	}
}


/*
 * 
 * 		if (toState == "Night") {
			for (int i = 0; i < enemies.transform.childCount; ++i) {
				GameObject enemyParent = enemies.transform.GetChild (i).gameObject;
				GameObject enemy = enemyParent.transform.FindChild ("Enemy").gameObject;
				if (enemyParent.name.Contains ("GoblinMelee")) {
					enemy.GetComponent<Enemy> ().characterStats = meleeGoblinNightStats; 
				} else if (enemyParent.name.Contains ("GoblinRanged")) {
					enemy.GetComponent<Enemy> ().characterStats = rangedGoblinNightStats; 
				} else if (enemyParent.name.Contains ("Bat")) {
					enemy.GetComponent<BatScript> ().characterStats = batNightStats; 
				}
			}
		} else if (toState == "Day") {
			for (int i = 0; i < enemies.transform.childCount; ++i) {
				GameObject enemyParent = enemies.transform.GetChild (i).gameObject;
				GameObject enemy = enemyParent.transform.FindChild ("Enemy").gameObject;
				if (enemyParent.name.Contains ("GoblinMelee")) {
					enemy.GetComponent<Enemy> ().characterStats = meleeGoblinDayStats; 
				} else if (enemyParent.name.Contains ("GoblinRanged")) {
					enemy.GetComponent<Enemy> ().characterStats = rangedGoblinDayStats; 
				} else if (enemyParent.name.Contains ("Bat")) {
					enemy.GetComponent<BatScript> ().characterStats = batDayStats; 
				}
			}
		}
 * 
 * */