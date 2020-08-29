using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSlowRocket : MonoBehaviour
{

    public float speed = 3f;
    public float lifeTime = 2;
    public Vector3 dir = new Vector3(-1, 0, 0);

    Vector3 stepVector;
    Rigidbody2D rbg;

    // Start is called before the first frame update
    void Start()
    {

        Destroy(gameObject, lifeTime);
        rbg = GetComponent<Rigidbody2D>();
        stepVector = speed * dir.normalized;
    }

    private void FixedUpdate()
    {
        rbg.velocity = stepVector;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
