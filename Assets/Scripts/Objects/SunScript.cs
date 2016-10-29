using UnityEngine;
using System.Collections;

public class SunScript : MonoBehaviour {
	private int dayLength;
	private int dayStart;
	private int nightStart;
	private int currentTime;
	public float cycleSpeed;
	private bool isDay;
	private Vector3 sunPosition;
	public Light sun;
	public GameObject earth;
	// Use this for initialization
	void Start () {
		dayLength = 1440;
		dayStart = 0;
		nightStart = 720;
		currentTime = 0;
		StartCoroutine (TimeOfDay());
		earth = gameObject.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentTime > 0 && currentTime < dayStart) {
			isDay = false;
			sun.intensity = 0;
		} else if (currentTime >= dayStart && currentTime < nightStart) {
			isDay = true;
			//sun.intensity = 1;
		} else if (currentTime >= nightStart && currentTime < dayLength) {
			isDay = false;
			sun.intensity = 0;
		} else if (currentTime >= dayLength) {
			currentTime = 0;
		}
		if (isDay) {
			if (currentTime < dayLength/4) {// morning
				sun.intensity = currentTime/(dayLength/4f);
			} else if (currentTime < nightStart) { // afternoon
				sun.intensity = -currentTime/(dayLength/4f) + 2;
			}
		}

		updateSunPosition ();

	}
	void updateSunPosition() {
		
		float currentTimeF = currentTime;
		float dayLengthF = dayLength;
		float yPos = 0;
		float xPos = -9f;
		if (currentTime < nightStart) { // day, sun should go up then down halfway through
			if (currentTime < nightStart / 2) { // sun goes up
				yPos = currentTimeF / nightStart*40 - 10;
				xPos = currentTimeF / nightStart * 20 - 9;
			} else { // sun goes down
				yPos = -currentTimeF / nightStart * 40 + 30 ;
				xPos = currentTimeF / nightStart * 20 - 9;
			}
		} else { // reset sun pos to right before it rises
			yPos = -12f;
		}
		earth.transform.localPosition = new Vector3 (xPos, yPos, earth.transform.localPosition.z);

		earth.transform.eulerAngles = new Vector3 (0,0,(-(currentTimeF / dayLengthF)*360) + 90);
	}
	IEnumerator TimeOfDay() {
		while (true) {
			currentTime += 1;
			int hours = Mathf.RoundToInt (currentTime / 60);
			int minutes = currentTime % 60;
			yield return new WaitForSeconds (1f / cycleSpeed);
		}
	}
}
