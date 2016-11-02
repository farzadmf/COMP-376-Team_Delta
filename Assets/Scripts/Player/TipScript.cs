using UnityEngine;
using System.Collections;

public class TipScript : MonoBehaviour {
	public int tipNumber;
	public bool activatedOnce;
	public bool activateAtNight;
	public int timeOn;
    [TextArea(7, 10)]
    public string message;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
