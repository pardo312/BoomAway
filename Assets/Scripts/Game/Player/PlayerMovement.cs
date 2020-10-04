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
        private bool isWalking;
        private bool initEndWalk;
        private Animator anim;
        void Start()
        {
            isWalking =false;
            initEndWalk=false;
            rb = GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if(!Grid.gameStateManager.editing){
                horizontalInput = Input.GetAxis("Horizontal");
                if(horizontalInput != 0)
                {
                    anim.SetBool("walking", true);
                }
                else
                {
                    anim.SetBool("walking", false);
                }
                flipSprite();
            }
        }
        void FixedUpdate()
        {
            if(!Grid.gameStateManager.editing){
                
                if(horizontalInput != 0){
                    initEndWalk=true;
                    rb.velocity = new Vector2(horizontalInput * speed * Time.deltaTime* 100, rb.velocity.y);
                    if(!isWalking && rb.velocity.y == 0)
                    {
                        Grid.audioManager.Play("WalkFX");
                        isWalking=true;
                    }
                }
                else{
                    if(initEndWalk){
                        rb.velocity = new Vector2(0, rb.velocity.y);
                        Grid.audioManager.StopPlaying("WalkFX");
                        isWalking=false;
                        initEndWalk=false;
                    }                
                }
            }
        }
        void flipSprite()
        {
            sr.flipX = horizontalInput < 0;
        }
    }
}

