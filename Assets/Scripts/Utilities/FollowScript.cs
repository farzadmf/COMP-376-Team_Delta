using UnityEngine;
using System.Collections;

/*
 * Used to follow a specified game object
 */
public class FollowScript : MonoBehaviour {

    [SerializeField]
    private GameObject followThisObject;
	
	// Update is called once per frame
	void Update () {
        transform.position = followThisObject.transform.position;
        transform.rotation = followThisObject.transform.rotation;
    }
}
