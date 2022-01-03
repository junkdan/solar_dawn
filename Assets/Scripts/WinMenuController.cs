using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinMenuController : MonoBehaviour
{
    private GameObject[] objects;
    private GameObject winBlackPane;
    private GameObject probe;
    private SatelliteController satelliteController;
    private bool winScreenCoroutineIsActive = false;

    private void Start()
    {
        objects = GameObject.FindGameObjectsWithTag("WinScreen");
        winBlackPane = GameObject.FindGameObjectWithTag("WinBlackPane");
        if (objects == null) Debug.Log("Sad");
        foreach(GameObject o in objects)
        {
            o.SetActive(false);
        }
    }

    void Update()
    {
        if (winScreenCoroutineIsActive) return;
        probe = GameObject.FindGameObjectWithTag("Probe");
        if(probe != null)
        {
            satelliteController = probe.GetComponent<SatelliteController>();
            if (satelliteController.IsWin())
            {
                StartCoroutine(ShowWinScreen());
            }
        }
    }

    IEnumerator ShowWinScreen()
    {
        winScreenCoroutineIsActive = true;
        foreach (GameObject o in objects)
        {
            o.SetActive(true);
        }
        winBlackPane.SetActive(true);
        yield return StartCoroutine(FadeBlackOutSquare(true, 5));
        winScreenCoroutineIsActive = false;
    }

    IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, int fadeSpeed = 5)
    {
        Color objectColor = winBlackPane.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while (winBlackPane.GetComponent<Image>().color.a < 0.9f)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                winBlackPane.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        else
        {
            while (winBlackPane.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                winBlackPane.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
    }
}
