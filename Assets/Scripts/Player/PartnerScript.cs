using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class PartnerScript : MonoBehaviour
{
    [SerializeField]
    private Text text;
    private Canvas bubble;

    private bool moved;

    // Use this for initialization
    void Start()
    {
        bubble = text.GetComponentInParent<Canvas>();
        bubble.enabled = false;

        text.GetComponent<ContentSizeFitter>().enabled = true;
        bubble.GetComponent<ContentSizeFitter>().enabled = true;

        moved = false;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Tip")
        {
			bool isDay = GameObject.Find ("Sun").transform.GetChild(0).GetComponent<SunScript>().isDay;
			if ((isDay == false && c.gameObject.GetComponent<TipScript> ().activateAtNight == true) ||
				(c.gameObject.GetComponent<TipScript> ().activateAtNight == false)) {
				showMessage (c.gameObject.GetComponent<TipScript> ().tipNumber,
					c.gameObject.GetComponent<TipScript> ().activatedOnce,
					c.gameObject.GetComponent<TipScript> ().message,
					c.gameObject.GetComponent<TipScript> ().timeOn);
				c.GetComponent<TipScript> ().activatedOnce = true;
			}
        }
    }

    void showMessage(int tipN, bool activatedOnce, string message, int howLong)
    {
        if (activatedOnce == false)
        {
            activateText(tipN, message, howLong);
        }
    }

    void activateText(int tipN, string message, int howLong)
    {
        if (IsInvoking("deleteText"))
        {
            CancelInvoke("deleteText");
        }
        setPosition(tipN);
        bubble.enabled = true;
        text.text = message;
        Quaternion rot = text.transform.rotation;
        Vector3 playerRot = transform.parent.rotation.eulerAngles;
        rot.eulerAngles = -playerRot;
        text.transform.localRotation = rot;

        if (tipN == 1)
        {
            Invoke("followUpFirstTip", howLong);
        }
        else
        {
            Invoke("deleteText", howLong);
        }
    }

    void deleteText()
    {
        bubble.enabled = false;
        text.text = "";
    }

    void followUpFirstTip()
    {
        if (!moved)
        {
            activateText(2, "I suppose you need to move.\nMust I teach you EVERYTHING?\n*Sighs* The left arrow key or A key moves\nyou to the left and the right arrow\nkey or D key moves you\nto the right.", 10);
        }
        else
        {
            activateText(3, "Good! You know how\nto move. At least you're not\ncompletely hopeless.", 5);
        }
    }

    void setPosition(int tipN)
    {
        switch (tipN)
        {
            case 1:
                bubble.transform.localPosition = new Vector3(6.53f, 6.44f, 0.0f);
                break;
            case 2:
                bubble.transform.localPosition = new Vector3(10.0f, 8.26f, 0.0f);
                break;
            case 3:
                bubble.transform.localPosition = new Vector3(7.17f, 5.95f, 0.0f);
                break;
            case 4:
                bubble.transform.localPosition = new Vector3(8.3f, 8.9f, 0.0f);
                break;
            case 5:
                bubble.transform.localPosition = new Vector3(6.4f, 4.42f, 0.0f);
                break;
            case 6:
                bubble.transform.localPosition = new Vector3(7.73f, 9.07f, 0.0f);
                break;
            case 7:
                bubble.transform.localPosition = new Vector3(7.93f, 7.53f, 0.0f);
                break;
            case 8:
                bubble.transform.localPosition = new Vector3(6.44f, 7.55f, 0.0f);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!moved && Input.GetButtonDown("Horizontal"))
            moved = true;
    }
}
