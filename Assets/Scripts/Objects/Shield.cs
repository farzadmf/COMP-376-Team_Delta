using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

    [SerializeField]
    GameObject blockPrefab;

    [SerializeField]
    Transform blockPrefabLocation;

    [SerializeField]
    GameObject parent;


    //Is called when this object triggers a collision
    public void OnTriggerEnter2D(Collider2D other)
    {
        //If it hit a shield then nulify this attack the collider will be reenabled when he does next attack
        if (other.tag == "EnemyDamage")
        {
            if (other.gameObject.transform.parent)
            {
                if (other.gameObject.transform.parent.gameObject.GetComponent<CombatCharacter>())
                {
                    other.gameObject.transform.parent.gameObject.GetComponent<CombatCharacter>().DisableMeleeCollider();
                    Instantiate(blockPrefab, blockPrefabLocation.position, Quaternion.identity);
                }
            }
             else
                 Destroy(other.gameObject);

            if (parent.gameObject.GetComponent<Character>())
            {
                parent.GetComponent<Character>().characterStats.decreaseStamina(other.gameObject.GetComponent<DamageDealer>().Attack.BaseDamage/4);
            }

        }

    
    }

}
