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

        //Switch sprite sides if scale changes to neg or pos
        if (followThisObject.transform.localScale.x >= 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);


    }
}
