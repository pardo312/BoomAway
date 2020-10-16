using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject GridLayout;
    private bool alredyChangeHorizontal;
    private bool alredyChangeVertical;
   [SerializeField] private int lenghtOfGrid;
   [SerializeField] private int heightOfGrid;
    // Start is called before the first frame update
    void Awake()
    {
        alredyChangeHorizontal = false;
        alredyChangeVertical = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal Movement
        if((int)playerTransform.position.x % lenghtOfGrid != 0)
            alredyChangeHorizontal = false;

        if(((int)playerTransform.position.x % lenghtOfGrid) == 0 && !alredyChangeHorizontal){
            alredyChangeHorizontal = true;
            transform.position = (Vector3)new Vector2( lenghtOfGrid*(int)(playerTransform.position.x/lenghtOfGrid),transform.position.y);
        }
        //Vertical Movement
        if((int)playerTransform.position.y % heightOfGrid != 0)
            alredyChangeVertical = false;

        if(((int)playerTransform.position.y % heightOfGrid) == 0 && !alredyChangeVertical){
            alredyChangeVertical = true;
            transform.position = (Vector3)new Vector2(transform.position.x, heightOfGrid*(int)(playerTransform.position.y/heightOfGrid));
        }
    }
}
