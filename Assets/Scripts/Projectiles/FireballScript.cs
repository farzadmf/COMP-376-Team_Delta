using UnityEngine;
using System.Collections;

public class FireballScript : MonoBehaviour {
	private string type; // "normal" or "exploding"
	private GameObject fireTrail;
	// Use this for initialization
	void Start () {
		fireTrail = transform.GetChild (0).GetChild (0).gameObject;

	}
	public void fireBallType(string t) {
		type = t;
		if (t == "normal") {
			GetComponent<DamageDealer> ().Attack.BaseDamage -= 15;
			GetComponent<DamageDealer> ().Attack.DamageOvertime.Damage = 1;
		}
	}
	void OnCollisionEnter2D(Collision2D c) {
		Debug.Log (c.gameObject.tag);
		if (c.gameObject.tag == "Enemy") {
			burnEffect (c);
		}
		if (type == "exploding") {
			GameObject g = (GameObject)Instantiate (Resources.Load ("Boom"));
			g.transform.position = transform.position;
		}
		Destroy (gameObject);
	}

    void OnTriggerEnter2D(Collider2D c)
    {
        
        if (c.gameObject.tag == "Enemy")
        {
            burnEffect(c);
        }
        if (c.gameObject.tag == "Enemy" || c.gameObject.tag == "Ground")
        {
            if (type == "exploding")
            {
                GameObject g = (GameObject)Instantiate(Resources.Load("Boom"));
                g.transform.position = transform.position;
            }

            Destroy(gameObject);
        }
        
    }

    void burnEffect(Collision2D c) {
		GameObject g = (GameObject)Instantiate (Resources.Load ("Fire1"));
		Vector3 enemyPos = c.gameObject.transform.position;
		int random = Random.Range (0,3);
		Vector3 newPos;
		if (random == 0)
			newPos = new Vector3 (enemyPos.x-0.2f,enemyPos.y-0.2f,0);
		else if (random == 1)
			newPos = new Vector3 (enemyPos.x,enemyPos.y+0.3f,0);
		else
			newPos = new Vector3 (enemyPos.x+0.2f,enemyPos.y,0);
		g.transform.position = newPos;
		g.transform.SetParent (c.gameObject.transform);
		g.transform.localPosition = new Vector3 (g.transform.localPosition.x,g.transform.localPosition.y,-1.2f);
	}

    void burnEffect(Collider2D  c)
    {
        GameObject g = (GameObject)Instantiate(Resources.Load("Fire1"));
        Vector3 enemyPos = c.gameObject.transform.position;
        int random = Random.Range(0, 3);
        Vector3 newPos;
        if (random == 0)
            newPos = new Vector3(enemyPos.x - 0.2f, enemyPos.y - 0.2f, 0);
        else if (random == 1)
            newPos = new Vector3(enemyPos.x, enemyPos.y + 0.3f, 0);
        else
            newPos = new Vector3(enemyPos.x + 0.2f, enemyPos.y, 0);
        g.transform.position = newPos;
        g.transform.SetParent(c.gameObject.transform);
        g.transform.localPosition = new Vector3(g.transform.localPosition.x, g.transform.localPosition.y, -1.2f);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
