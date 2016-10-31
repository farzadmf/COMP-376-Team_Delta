using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class PartnerScript : MonoBehaviour {
    [SerializeField]
	private Text text;
    private Canvas bubble;

    private Vector3 rot;

	// Use this for initialization
	void Start () {
        bubble = text.GetComponentInParent<Canvas>();
        bubble.enabled = false;

        rot = text.transform.rotation.eulerAngles;
	}

	void OnTriggerEnter2D(Collider2D c) {
		if (c.tag == "Tip") {
			showMessage(c.gameObject.GetComponent<TipScript>().tipNumber,c.gameObject.GetComponent<TipScript>().activatedOnce
				,c.gameObject.GetComponent<TipScript>().message,c.gameObject.GetComponent<TipScript>().timeOn);
			c.GetComponent<TipScript>().activatedOnce = true;
		}
	}

	void showMessage(int tipN, bool activatedOnce, string message, int howLong) {
		if (activatedOnce == false) {
            setPosition(tipN);
            bubble.enabled = true;
			activateText (message, howLong);
		}
	}

	void activateText(string message, int howLong) {
		if (IsInvoking ("deleteText")) {
			CancelInvoke ("deleteText");
		}
		text.text = message;
		Quaternion rot = text.transform.rotation;
		Vector3 playerRot = transform.parent.rotation.eulerAngles;
		rot.eulerAngles = -playerRot;
		text.transform.localRotation = rot;
		Invoke ("deleteText",howLong);
	}

    void deleteText()
    {
        bubble.enabled = false;
		text.text = "";
	}

    void setPosition(int tipN)
    {
        switch (tipN)
        {
            case 1:
                bubble.transform.localPosition = new Vector3(6.53f, 6.44f, 0.0f);
                break;
            case 2:
                bubble.transform.localPosition = new Vector3(6.53f, 8.5f, 0.0f);
                break;
            case 3:
                break;
            case 4:
                bubble.transform.localPosition = new Vector3(6.53f, 8.5f, 0.0f);
                break;
            default:
                break;
        }
    }

	// Update is called once per frame
	void Update () {
        
	}
}
