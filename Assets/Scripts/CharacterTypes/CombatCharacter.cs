using UnityEngine;
using System.Collections;

public abstract class CombatCharacter : Character {

    //Used to assign a projectile prefab to the combat character
    [SerializeField]
    private GameObject projectilePrefab;

    //projectiles initial position
    [SerializeField]
    private Transform projectilePosition;

    //Used to set the different states of the character
    public bool IsBasicAttack { get; set; }
    public bool IsRangedAttack { get; set; }


    // Initialization
    public override  void Start () {
        //Call the start method of the parent class
        base.Start();

    }
	
	// Update is called once per frame
	void Update () {
    
    }
    
    public override void HandleMovement()
    {
            //Call base class's move
            base.HandleMovement();
    }

    public void HandleBasicAttack()
    {
       
    }

    //Initializes a new projectile prefab into the world 
    public void TriggerProjectile()
    {
        if (isFacingRight)
        {
            //Create a new projectile using the projectile prefab
            GameObject temporaryProjectile = (GameObject)Instantiate(projectilePrefab, projectilePosition.position, Quaternion.identity);

            //Initialize variables of the projectile                       //Direction
            temporaryProjectile.GetComponent<BasicProjectile>().Initialize(Vector2.right);
        }
        else
        {
            //Create a new projectile using the projectile prefab
            GameObject temporaryProjectile = (GameObject)Instantiate(projectilePrefab, projectilePosition.position, Quaternion.identity);

            //Initialize variables of the projectile                       //Direction
            temporaryProjectile.GetComponent<BasicProjectile>().Initialize(Vector2.left);
        }

    }

}
