using BoomAway.Assets.Scripts.Game.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    //For editor purposes
    public float launchForce = 20f;
    private float invulnerableTime = 3f;    //Changing this will desync the damage animation

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(Grid.gameStateManager.editing == false)
        {
            GameObject other = collision.gameObject;

            //Determina si la colisión vino de la derecha o de la izquierda
            bool otherIsLeft = other.transform.position.x < transform.position.x;

            //Si el jugador toca las puas, "salta" en 45º hacia la dieccion donde las toco
            if(other.name.Equals("Player") && !(Grid.gameStateManager.damaged)) 
            {
                if (otherIsLeft)
                    other.GetComponent<Rigidbody2D>().AddForce(new Vector2(-launchForce, launchForce));
                else
                    other.GetComponent<Rigidbody2D>().AddForce(new Vector2(launchForce, launchForce));

                Grid.gameStateManager.health -= 0.2f;
                Grid.gameStateManager.damaged = true;
                Grid.audioManager.Play("PlayerDamage");
            }
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player") && Grid.gameStateManager.damaged)
            Invoke("resetDamage", invulnerableTime);
    }

    //After taking damage, the Player will have some time where they take no damage
    //This countdown starts the moment the player leaves
    private void resetDamage()
    {
        Grid.gameStateManager.damaged = false;
    }
}
