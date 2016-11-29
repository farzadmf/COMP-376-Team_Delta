using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class PartnerScript : MonoBehaviour
{
    [SerializeField]
    private Text text;
    private Canvas bubble;

    private bool moved;
    private bool firstNight;
    private float mTime;

    // Use this for initialization
    void Start()
    {
        bubble = text.GetComponentInParent<Canvas>();
        bubble.enabled = false;

        text.GetComponent<ContentSizeFitter>().enabled = true;
        bubble.GetComponent<ContentSizeFitter>().enabled = true;

        moved = false;

        if (GameObject.Find("Player").transform.position.x < 80)
        {
            firstNight = true;
        }
        else
        {
            firstNight = false;
        }

        mTime = 0.0f;
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
                    c.gameObject.GetComponent<TipScript>().pauseToRead,
					c.gameObject.GetComponent<TipScript> ().message,
					c.gameObject.GetComponent<TipScript> ().timeOn);
				c.GetComponent<TipScript> ().activatedOnce = true;
			}
        }
    }

    void showMessage(int tipN, bool activatedOnce, bool pauseToRead, string message, int howLong)
    {
        if (!activatedOnce)
        {
            if (pauseToRead)
            {
                if (GameObject.Find("Overlay").GetComponent<OverlayScript>().AreTipsOn())
                {
                    string[] msgs;
                    if (tipN == 1) {
                        msgs = new string[2];
                        msgs[0] = message;
                        msgs[1] = "I suppose you need to move.\nMust I teach you EVERYTHING?\n*Sighs* The left arrow key or A key\nmoves you to the left and the right\narrowkey or D key moves you\nto the right.";
                    }
                    else if (tipN == 7)
                    {
                        msgs = new string[4];
                        msgs[0] = message;
                        msgs[1] = "You can view the items you've picked\nup in your inventory... What? You don't\nknow how to open your inventory,\nseriously? You press the I button on\nyour keyboard, duh.";
                        msgs[2] = "If you don't even know how to open\nyour inventory, you probably don't\nknow how to use your hotkeys...\nMust I explain everything to you?\n*Sighs*";
                        msgs[3] = "Once you have your inventory open,\nyou can drag and drop the items you\nwant to use in your hotkeys on the\nbottom of the screen. Then use the\nnumbers on the top of your keyboard\nto activate the item there.";
                    }
                    else if (tipN == -1)
                    {
                        msgs = new string[5];
                        msgs[0] = "Oh yeah! This is your first night here. The transition between night and day\nis signified by a red flash. I guess I\nshould tell you what happens when\nit's night. Where do I start,\nhmm...";
                        msgs[1] = "Well you obviously know that you\ntransform at night because of all the\nexperiments done to you. I guess it's\nbecause you change form that you can\nno longer hold weapons. Of course\nyou can still use them during the\ndaytime.";
                        msgs[2] = "What? You still want to be able to\nattack? Of course you can still attack...\nI know what I said! You can't hold\nweapons but you have a claw there\ndon't you? Geez, sometimes I\nwonder why I put up with you.";
                        msgs[3] = "One more thing, you're not the only\none who can transform at night. Some\nof the other experiments can as well\nand there are some scary ones that\ncan only come out at night. On the\nbright side, you can use magic! Only during nightime though...";
                        msgs[4] = "\"How?\" you ask, obviously by right\nclicking the place you want the magic\nto go to, but you have to be facing that\ndirection. I've never heard of magic\nbeing cast behind the castor.";
                    }
                    else
                    {
                        msgs = new string[1];
                        msgs[0] = message;
                    }
                    GameObject.Find("Overlay").GetComponent<OverlayScript>().DisplayOverlay(msgs);
                }
            }
            else
            {
                activateText(tipN, message, howLong);
            }
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

        Invoke("deleteText", howLong);
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
            case 5:
                bubble.transform.localPosition = new Vector3(6.4f, 4.42f, 0.0f);
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

        if (!(GameObject.Find("RealSun").GetComponent<SunScript>().isDay) && firstNight && mTime > 0.5f)
        {
            firstNight = false;
            showMessage(-1, false, true, "", -1); // message about nighttime
        }

        if (firstNight)
            mTime += Time.deltaTime;
    }
}
