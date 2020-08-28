using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShowUI : MonoBehaviour
{
    // Update is called once per frame
    public void HideUIElement(GameObject UIelementToChange)
    {
        UIelementToChange.SetActive(false);
    }
    public void ShowUIElement(GameObject UIelementToChange)
    {
        UIelementToChange.SetActive(true);
    }
    public void PauseTime()
    {
         Time.timeScale = 0;
    }
    public void UnPauseTime()
    {
         Time.timeScale = 1;
    }
}
