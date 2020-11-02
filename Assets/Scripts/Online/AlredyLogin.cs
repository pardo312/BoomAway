using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlredyLogin : MonoBehaviour
{
    
    [SerializeField]private GameObject onlineMenu;
    void Start()
    {
        if(Grid.gameStateManager.usernameOnline!= "Anonymous")
        {
            onlineMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }

}
