using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    [SerializeField]
    int level;
    private bool start;

    private Vector2 velocity;

    public float smoothTimeY;
    public float smoothTimeX;

    private GameObject player;

    public bool bounds;
    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
        start = true;
	}
   
	// Update is called once per frame
	void Update () {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

        switch (level)
        {
            case 1:
                if (start)
                {
                    start = false;
                    posX = Mathf.SmoothDamp(transform.position.x, 3.496513f, ref velocity.x, smoothTimeX);
                    transform.position = new Vector3(posX, posY, transform.position.z);
                }
                else if (player.transform.position.x > 3.481 && player.transform.position.x <= 169.465)
                {
                        transform.position = new Vector3(posX, posY, transform.position.z);
                }
                else if (player.transform.position.x >= 169.465)
                {
                    posX = Mathf.SmoothDamp(transform.position.x, 180.1122f, ref velocity.x, smoothTimeX);
                    if (transform.position.x != 180.1122)
                    {
                        transform.position = new Vector3(posX, posY, transform.position.z);
                    }
                }
                break;
            default:
                transform.position = new Vector3(posX, posY+6, transform.position.z);

                if (bounds)
                {
                    transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                                                     Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                                                     Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));

                }
                break;
        }

    }
}
