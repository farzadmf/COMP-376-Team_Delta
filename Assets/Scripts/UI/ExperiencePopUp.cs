using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExperiencePopUp : MonoBehaviour {

    [SerializeField]
    private Text expText;

	// Use this for initialization
	void Start () {
        expText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {}

    public void DisplayExpValue()
    {
        expText.text = GameObject.Find("Player").GetComponent<PlayerScript>().getExpPercentage();
        expText.enabled = true;
    }

    public void HideExpValue()
    {
        expText.enabled = false;
    }
}
