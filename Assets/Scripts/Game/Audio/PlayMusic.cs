using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    [SerializeField]private string MusicName;
    // Start is called before the first frame update
    void Start()
    {
        Grid.audioManager.StopPlayingAll();
        Grid.audioManager.Play(MusicName);
    }

   
}
