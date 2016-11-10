using UnityEngine;
using System.Collections;

public class FlickeringFixScript : MonoBehaviour {
	private float factor;

	void Start () {
		//if ( factor == 0)
		factor = 0.005f;
		Vector3 v = transform.localScale;
		v.x += factor;
		v.y += factor;
		transform.localScale = v;

		float newZ = Random.Range(transform.position.z,transform.position.z+0.2f);
		Vector3 vv = transform.position;
		vv.z = newZ;
		transform.position = vv;
	}


}
