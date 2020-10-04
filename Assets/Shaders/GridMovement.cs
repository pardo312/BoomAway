using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject GridLayout;
    private bool alredyChange;
   [SerializeField] private int lenghtOfGrid = 37;
    
    [SerializeField]private int moveGridBy;
    // Start is called before the first frame update
    void Awake()
    {
        alredyChange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Grid.gameStateManager.editing)
            GridLayout.SetActive(false);
        else
            GridLayout.SetActive(true);

        
        if((int)playerTransform.position.x % lenghtOfGrid != 0)
            alredyChange = false;

        if(((int)playerTransform.position.x % lenghtOfGrid) == 0 && !alredyChange){
            alredyChange = true;
            if(playerTransform.position.x>0)
                transform.position = (Vector3)new Vector2( moveGridBy*(int)(playerTransform.position.x/lenghtOfGrid),0);
            else
                transform.position -= (Vector3)new Vector2( moveGridBy*(int)(playerTransform.position.x/lenghtOfGrid),0);
        }
    }
}
