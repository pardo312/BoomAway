using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    private GameObject player;
    private SpriteRenderer sr;
    private BoxCollider2D bc;
    private bool alredyTransformPlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        alredyTransformPlayerPos=false;
        player = GameObject.Find("Player");
        sr = gameObject.GetComponent<SpriteRenderer>();
        bc = gameObject.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(GameObject.Find("SpawnPoint(Clone)") == null)
        {
            bool editing = Grid.gameStateManager.editing;
            if (!editing){
                if(!alredyTransformPlayerPos){
                    player.transform.position = gameObject.transform.position;
                    sr.enabled =false;
                    bc.enabled = false;
                    alredyTransformPlayerPos= true;
                }
            } 
            else{
                alredyTransformPlayerPos= false;
                sr.enabled =true;
                bc.enabled = true;
            }

        }
       
    }

}
