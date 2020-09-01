using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeButtonColor : MonoBehaviour
{
    private Image btn;
    [SerializeField]private GameObject tmpGameObject;
    private TextMeshProUGUI tmp;
    // Start is called before the first frame update
    void Awake()
    {
        btn = GetComponent<Image>();   
        tmp= tmpGameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Grid.gameStateManager.editing){
            btn.color = Color.cyan;
            tmp.text = "Editing";
        }
        else{
            btn.color = Color.green;
            tmp.text = "Playing";
        }
    }
}
