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
			showMessage(c.gameObject.GetComponent<TipScript>().tipNumber,c.gameObject.GetComponent<TipScript>().activatedOnce
				,c.gameObject.GetComponent<TipScript>().message,c.gameObject.GetComponent<TipScript>().timeOn);
			c.GetComponent<TipScript> ().activatedOnce = true;
		}
	}

	void showMessage(int i, bool activatedOnce, string message, int howLong) {
		if (activatedOnce == false) {
			activateText (message, howLong);
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
