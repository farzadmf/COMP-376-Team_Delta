using UnityEngine;
using System.Collections;

public class IgnoreCollision : MonoBehaviour {

    //Set in unity another object which you don't want to collide with (Usually the player, if you don't want the enemies to collide with the player)
    [SerializeField]
    private Collider2D otherObject;

    //Called only after all objects are loaded, called only once
    public void Awake()
    {
        //Ignore collision of this collider and the otherObjects collider
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), otherObject, true);
    }
}
