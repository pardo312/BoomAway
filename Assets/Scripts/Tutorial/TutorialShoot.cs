using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialShoot : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();        
    }

    private void OnEnable()
    {
        ActivateAnimAndChildren(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ActivateAnimAndChildren(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ActivateAnimAndChildren(false);
        }
    }

    private void ActivateAnimAndChildren(bool activate)
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(activate);

        anim.enabled = activate;
    }
}
