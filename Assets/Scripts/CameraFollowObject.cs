using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    public GameObject gameObjectToFollow = null;
    private GameObject rocket;
    private GameObject probe;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObjectToFollow == null) return;
        rocket = GameObject.FindGameObjectWithTag("Rocket");
        probe = GameObject.FindGameObjectWithTag("Probe");
        if (rocket != null)
        {
            gameObject.transform.position = rocket.transform.position + new Vector3(0f, 0f, -10f);
        }
        if (probe != null)
        {
            gameObject.transform.position = probe.transform.position + new Vector3(0f, 0f, -10f);
        }
        if (rocket == null && probe == null)
        {
            gameObject.transform.position = gameObjectToFollow.transform.position + new Vector3(0f, 0f, -10f);
        }
    }
}
