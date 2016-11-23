using UnityEngine;
using System.Collections;

public class triggerMusic : MonoBehaviour {

    [SerializeField]
    private int index;

    private Music parent;

    private float wait;

    private float delaybetweenActivations;

    // Use this for initialization
    void Start () {
        parent = transform.parent.gameObject.GetComponent<Music>();
        wait = 0;
        delaybetweenActivations = 0.5f; 

    }

    //For Trigger Collisions
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (wait < Time.time)
        {
            if (other.gameObject.tag == "Player")
                parent.ChangeMusic(index);

            wait = Time.time + delaybetweenActivations;
        }
       
    }
}
