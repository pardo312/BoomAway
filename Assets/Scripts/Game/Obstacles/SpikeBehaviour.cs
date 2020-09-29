using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBehaviour : MonoBehaviour
{

    public float launchForce = 20f;

    private bool alreadyLaunched = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        //Determina si la colisión vino de la derecha o de la izquierda
        bool otherIsLeft = other.transform.position.x < transform.position.x;

        //Si rel jugador toca las puas, "saltara" en 45º hacia la dieccion donde las toco
        if(other.name.Equals("Player") || other.CompareTag("Explosive")) // && !alreadyLaunched)
        {
            if (otherIsLeft)
                other.GetComponent<Rigidbody2D>().AddForce(new Vector2(-launchForce, launchForce));
            else
                other.GetComponent<Rigidbody2D>().AddForce(new Vector2(launchForce, launchForce));

            alreadyLaunched = true;
            Grid.gameStateManager.health -= 0.2f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Garantiza la fuerza se aplique una sola vez
        if (collision.gameObject.name.Equals("Player"))
            alreadyLaunched = false;
    }
}
