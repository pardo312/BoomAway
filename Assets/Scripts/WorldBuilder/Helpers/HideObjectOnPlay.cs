using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObjectOnPlay : MonoBehaviour
{
    [SerializeField]private GameObject objectToHide;
    // Update is called once per frame
    void Update()
    {
        if(Grid.gameStateManager.editing){
            objectToHide.SetActive(true);
        }
        else{
            objectToHide.SetActive(false);
        }
    }
}
