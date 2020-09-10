using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoomAway.Assets.Scripts.Game.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Controls")]
        [SerializeField] private float speed;

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
            if(!Grid.gameStateManager.editing){
                horizontalInput = Input.GetAxis("Horizontal");
                flipSprite();
            }
        }
        void FixedUpdate()
        {
            if(!Grid.gameStateManager.editing){
                if(horizontalInput != 0)
                    rb.velocity = new Vector2(horizontalInput * speed * Time.deltaTime* 100, rb.velocity.y);
            }
        }
        void flipSprite()
        {
            sr.flipX = horizontalInput < 0;
        }
    }
}

