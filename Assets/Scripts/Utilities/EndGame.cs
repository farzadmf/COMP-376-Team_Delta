using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndGame : MonoBehaviour {

    public  void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }

    
}
