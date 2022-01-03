using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextMissionButton : MonoBehaviour
{
    public Button nextMissionButton;

    void Start()
    {
        Button btn = nextMissionButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Time.timeScale = 1f;

    }
}
