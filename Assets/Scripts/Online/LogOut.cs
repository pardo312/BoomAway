using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogOut : MonoBehaviour
{
    
    [SerializeField]private GameObject onlineLoginMenu;
    // Start is called before the first frame update
    public void logOutOnline()
    {
        Grid.gameStateManager.usernameOnline = "";
        transform.parent.gameObject.SetActive(false);
        onlineLoginMenu.SetActive(true);
    }
}
