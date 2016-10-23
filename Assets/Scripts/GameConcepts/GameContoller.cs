using UnityEngine;
using System.Collections;

public class GameContoller : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InitializeVariables();
    }
	
    private void InitializeVariables()
    {
        DamagePopUpController.Initialize();
    }
}
