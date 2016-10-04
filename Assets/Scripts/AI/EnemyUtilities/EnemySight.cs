using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

    //Handle to the enemy which to attach sigth
    [SerializeField]
    private Enemy thisEnemy;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //If player collides with sight then traget him
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!other.gameObject.GetComponent<Character>().IsDead())
            {
                thisEnemy.Target = other.gameObject;
            }
            
        }
    }

    //When player exits sight then untarget player
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            thisEnemy.Target = null;
        }
    }
}
