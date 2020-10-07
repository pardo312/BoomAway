using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRocket : MonoBehaviour
{
    void OnEnable()
    {
        ActivateChildren(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            ActivateChildren(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ActivateChildren(false);
        }
    }

    private void ActivateChildren(bool activate)
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(activate);
    }
}
