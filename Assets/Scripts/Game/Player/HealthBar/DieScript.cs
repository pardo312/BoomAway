using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieScript : MonoBehaviour
{
    [SerializeField]private GameObject DieMenu;

    // Update is called once per frame
    void Update()
    {
        if(Grid.gameStateManager.health<0.1f){
            DieMenu.SetActive(true);
            Time.timeScale = 0f;
            Grid.gameStateManager.IsPaused = true;
        }
    }
}
