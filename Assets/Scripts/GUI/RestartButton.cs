using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    public Button restartButton;
    public string sceneKeyValue = "0";

    void Start()
    {
        Button btn = restartButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Enum.TryParse(sceneKeyValue, out Loader.Scene scene);
        Loader.Load(scene);
        Time.timeScale = 1f;
    }
}
