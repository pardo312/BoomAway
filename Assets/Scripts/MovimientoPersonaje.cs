using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
    Rigidbody2D rgb;
    Animator animator;
    bool slowrocket;

    public GameObject slowrocketPrototype;
    public GameObject fastrocketPrototype;

    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Disparar();
        }
    }

    public void Disparar()
    {
        //animator.SetTrigger("apuntar");
        bool slowrocket = true;
        if (slowrocket)
        {
            EmitirBala(slowrocketPrototype);
        }
        
    }

    public void EmitirBala(GameObject prototype)
    {
        if (slowrocket)
        {
            GameObject objectCopy = Instantiate(prototype);
            objectCopy.transform.position = new Vector3(transform.localPosition.x, transform.localPosition.y, 1);
            objectCopy.GetComponent<ControlSlowRocket>().dir = new Vector3(transform.localScale.x, 0, 0);
        }
    }

}
