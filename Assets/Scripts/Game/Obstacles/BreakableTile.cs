using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableTile : MonoBehaviour
{

    public bool explode;
    public LayerMask layerToExplode;
    private Animator anim;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (explode)
        {

            anim.SetTrigger("Explotion");
            Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, 2, layerToExplode);


            StartCoroutine(ExecuteAfterTime(0.75f, objects));
        }
    }

    IEnumerator ExecuteAfterTime(float time, Collider2D[] objects)
    {
        yield return new WaitForSeconds(time);
        foreach (Collider2D obj in objects)
        {
            obj.GetComponent<BreakableTile>().explode = true;
        }
        gameObject.SetActive(false);
    }

}
