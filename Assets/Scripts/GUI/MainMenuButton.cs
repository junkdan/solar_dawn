using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    public Button mainMenuButton;

    void Start()
    {
        Button btn = mainMenuButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Enum.TryParse("2", out Loader.Scene scene);
        Loader.Load(scene);
        Time.timeScale = 1f;
    }
}
