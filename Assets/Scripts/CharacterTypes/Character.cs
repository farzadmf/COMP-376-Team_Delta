using UnityEngine;
using System.Collections;


public abstract class Character : MonoBehaviour {


    public Animator ThisAnimator { get; private set; }

    [SerializeField]
    protected float movementSpeed;

    //If not faceing right then facing left
    protected bool isFacingRight;

    // Use this for initialization
    public virtual void Start () {

        //By default a character will start by facing right
        isFacingRight = true;
        ThisAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void HandleMovement()
    {
    }

    public void ChangeDirection()
    {
        //Change facing direction to the opposite of what it currently is
        isFacingRight = !isFacingRight;

        //Change to oposite direction
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;

    }
}
