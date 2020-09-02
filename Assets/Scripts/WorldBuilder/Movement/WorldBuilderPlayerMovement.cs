using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilderPlayerMovement : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private bool inEditMode;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Grid.gameStateManager.editing)
        {
            rb.bodyType = RigidbodyType2D.Static;
            transform.position += new Vector3(Input.GetAxis("Horizontal")*speed*Time.deltaTime,
            Input.GetAxis("Vertical")*speed*Time.deltaTime,0);
            inEditMode=true;
        }
        else{
            if(inEditMode){
                inEditMode= false;
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

}
