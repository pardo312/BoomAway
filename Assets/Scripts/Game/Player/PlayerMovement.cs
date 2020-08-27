using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField]private float speed;

    private float horizontalInput;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput  = Input.GetAxis("Horizontal");
        flipSprite();
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput*speed,rb.velocity.y);
    }
    void flipSprite()
    {
        sr.flipX = horizontalInput < 0;
    }
}
