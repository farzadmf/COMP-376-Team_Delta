using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

    private PlayerControllerScript player;

	// Use this for initialization
	void Start () {

        player = gameObject.GetComponentInParent<PlayerControllerScript>();
	}
	
	void OnTriggerEnter2D(Collider2D col)
    {
        player.touchingGround = true;

    }

    void OnTriggerExit2D(Collider2D col)
    {
        player.touchingGround = false;
    }
}
