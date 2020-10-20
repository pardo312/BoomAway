using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("FadeIn",false);
        animator.SetBool("FadeOut",true);
    }
}
