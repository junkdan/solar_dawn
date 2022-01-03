using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetButton : MonoBehaviour
{
    public Camera cam;
    public GameObject target;
    public Button targetButton;
    private CameraFollowObject cameraFollowObject;
    private bool canPlay;

    void Start()
    {
        Button btn = targetButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        cameraFollowObject = cam.GetComponent<CameraFollowObject>();
    }

    void TaskOnClick()
    {
        Debug.Log("You have clicked the target button");
        canPlay = GameObject.Find("PS_Planet_Earth").GetComponent<Controller>().canPlay;
        if (GameObject.FindGameObjectWithTag("Rocket") == null && GameObject.FindGameObjectWithTag("Probe") == null && canPlay)
        {
            cameraFollowObject.gameObjectToFollow = target;
        }
    }

}
