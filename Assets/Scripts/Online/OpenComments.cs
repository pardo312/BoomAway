using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenComments : MonoBehaviour
{

    public string level;
    public GameObject commentMenu;
    public GameObject onlineMenu;

    public void showHide()
    {
        commentMenu.SetActive(true);
        onlineMenu.SetActive(false);
    }

}
