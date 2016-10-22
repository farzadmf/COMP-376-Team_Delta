using UnityEngine;
using System.Collections;
public class PartnerScript : MonoBehaviour {
	private GameObject text;
	// Use this for initialization
	void Start () {
		text = transform.GetChild (0).gameObject;
	}

	void OnTriggerEnter2D(Collider2D c) {
		if (c.tag == "Tip") {
			showMessage(c.gameObject.GetComponent<TipScript>().tipNumber,c.gameObject.GetComponent<TipScript>().activatedOnce);
			c.GetComponent<TipScript> ().activatedOnce = true;
		}
	}

	void showMessage(int i, bool activatedOnce) {
		string message = "";
		if (activatedOnce == false) {
			if (i == 1) {
				message = "Loler";
				activateText (message, 4);
			} else if (i == 2) {
				message = "Ho Ho Ho, merry Christmas";
				activateText (message, 8);
			}
		}
	}
	void activateText(string message, int howLong) {
		if (IsInvoking ("deleteText")) {
			CancelInvoke ("deleteText");
		}
		text.GetComponent<TextMesh>().text = message;
		Quaternion rot = text.transform.rotation;
		Vector3 playerRot = transform.parent.rotation.eulerAngles;
		rot.eulerAngles = -playerRot;
		text.transform.localRotation = rot;
		Invoke ("deleteText",howLong);
	}
	void deleteText() {
		text.GetComponent<TextMesh> ().text = "";
	}
	// Update is called once per frame
	void Update () {
		
	}
}
