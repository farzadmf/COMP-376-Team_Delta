using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour {

    private Vector3 mousePosition;

    [SerializeField]
    float maxZ,minZ;

    [SerializeField]
    Character character;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    
        var dir = mousePosition - transform.position;

        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (maxZ != 0 && minZ != 0)
            angle = Mathf.Clamp(angle, minZ, maxZ);

        if (!character.CharacterIsFacingRight())
            transform.localScale = new Vector3 (-1,1,1) ;
        else
            transform.localScale = new Vector3(1, 1, 1);

        Debug.Log(!character.CharacterIsFacingRight());

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.rotation = Quaternion.AngleAxis(angle, character.CharacterIsFacingRight() ? Vector3.forward: Vector3.up);
    }
}
