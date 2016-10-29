using UnityEngine;
using System.Collections;

public class BossWallScript : MonoBehaviour {

    [SerializeField]
    bool fallen;
    private GameObject player;
    private Animation anim;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animation>();
        anim.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
