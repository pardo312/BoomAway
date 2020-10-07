using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoheteFalsoSpawns : MonoBehaviour
{
    public float init_speed;
    private Rigidbody2D rgb;

    void Awake()
    {
        rgb = GetComponent<Rigidbody2D>();
        rgb.velocity = new Vector2(init_speed, 0f);
        if(init_speed < 0)
        {
            Vector2 scale_before = gameObject.transform.localScale;
            scale_before.x *= -1;
            gameObject.transform.localScale = scale_before;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
