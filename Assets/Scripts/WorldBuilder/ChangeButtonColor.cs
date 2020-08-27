using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonColor : MonoBehaviour
{
    private Image btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Image>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(Grid.gameStateManager.editing)
            btn.color = Color.cyan;
        else
            btn.color = Color.green;
    }
}
