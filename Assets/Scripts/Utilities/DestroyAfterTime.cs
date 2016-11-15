using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

    [SerializeField]
    private float timer;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, timer);
	}
	
}
