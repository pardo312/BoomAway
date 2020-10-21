using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSound : MonoBehaviour
{

    // Update is called once per frame
    public void playSound()
    {
        Grid.audioManager.Play("ButtonFX");
    }
}
