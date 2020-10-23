﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoomAway.Assets.Scripts.Game.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [HideInInspector]public float speed = Constants.PLAYER_SPEED;

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

                anim.SetBool("walking", horizontalInput != 0);
                anim.SetBool("damaged", Grid.gameStateManager.damaged);
            }
        }
        void FixedUpdate()
        {
            if(!Grid.gameStateManager.editing){
                
                if(horizontalInput != 0){
                    initEndWalk=true;
                    rb.velocity = new Vector2(horizontalInput * speed * Time.deltaTime* 100, rb.velocity.y);
                    if(!isWalking && rb.velocity.y == 0){
                        Grid.audioManager.Play("WalkFX");
                        isWalking=true;
                    }
                    if(horizontalInput <0){
                        sr.flipX = true;
                    }
                    else if(horizontalInput >0){
                        sr.flipX = false;
                    }
                }
                else{
                    if(initEndWalk){
                        if(rb.velocity.x <0)
                            flipSprite();
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
            sr.flipX = horizontalInput <= 0;
        }
    }
}

