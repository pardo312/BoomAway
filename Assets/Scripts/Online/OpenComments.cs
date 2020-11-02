using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenComments : MonoBehaviour
{

    public string level;
    public GameObject commentMenu;
    public GameObject onlineMenu;
    public CreateComment create;

    public void showHide()
    {
        commentMenu.SetActive(true);
        if(onlineMenu!= null)
            onlineMenu.SetActive(false);
    }

    public void updateLevel()
    {
        create.updateLevel(level);
    }

}
