using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OverlayScript : MonoBehaviour {

    private bool tips;
    private Text tipText;
    private int currentTip;
    private GameObject display;
    private GameStateScript gss;
    string[] instructions;

	// Use this for initialization
	void Start () {
        tips = true;
        tipText = GameObject.Find("OverlaySpeech").GetComponentInChildren<Text>();
        currentTip = 0;
        display = gameObject.GetComponentInChildren<Image>().gameObject;
        gss = GameObject.Find("GameState").GetComponent<GameStateScript>();
        HideOverlay();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnGUI() {
        if (display.activeInHierarchy)
        {
            Rect nextRect = new Rect(Screen.width * 0.45f, Screen.height * 0.65f, Screen.width * 0.15f, Screen.height * 0.075f);
            Rect returnRect = nextRect;
            returnRect.y = nextRect.y + nextRect.height + 20;
            Rect offRect = returnRect;
            offRect.y = returnRect.y + returnRect.height + 20;

            GUIStyle customButton = new GUIStyle("button");
            customButton.fontSize = 14;
            GUI.backgroundColor = Color.white;

            if (GUI.Button(nextRect, "Continue", customButton))
            {
                Next();
            }
            else if (GUI.Button(returnRect, "Return To Game", customButton))
            {
                ReturnToGame();
            }
            else if (GUI.Button(offRect, "Turn Off Tips", customButton))
            {
                TurnOffTips();
            }
        }
    }

    void HideOverlay()
    {
        if (display.activeInHierarchy)
        {
            tipText.text = "";
            display.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerControllerScript>().enabled = true;
        }
    }

    void ShowOverlayTip(string t)
    {
        tipText.text = t;
    }

    void Next()
    {
        if (currentTip < instructions.Length)
        {
            ShowOverlayTip(instructions[currentTip]);
            currentTip += 1;
        }
        else
        {
            ReturnToGame();
        }
    }

    void ReturnToGame()
    {
        HideOverlay();
        gss.resumeGameForTip();
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().enabled = true;
    }

    void TurnOffTips()
    {
        tips = false;
        ReturnToGame();
    }

    void TurnOffCamera()
    {
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().enabled = false;

        gss.pauseGameForTip();
        Next();
    }

    public void DisplayOverlay(string[] text, int tipN)
    {
        display.SetActive(true);
        instructions = text;
        currentTip = 0;
        GameObject.Find("Player").GetComponent<PlayerControllerScript>().enabled = false;

        if (tipN == 1 && GameObject.Find("Main Camera").GetComponent<CameraFollow>().enabled)
        {
            Invoke("TurnOffCamera", 0.1f);
        }
        else
        {
            TurnOffCamera();
        }
    }

    public bool AreTipsOn()
    {
        return tips;
    }
}
