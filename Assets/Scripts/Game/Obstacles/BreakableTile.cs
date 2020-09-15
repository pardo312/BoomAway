using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableTile : MonoBehaviour
{

    public bool explode;
    public LayerMask layerToExplode;

    private void Update()
    {
        if (explode)
        {
            Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, 2, layerToExplode);
            
            StartCoroutine(ExecuteAfterTime(0.25f, objects));
        }
    }

    IEnumerator ExecuteAfterTime(float time, Collider2D[] objects)
    {
        yield return new WaitForSeconds(time);
        foreach (Collider2D obj in objects)
        {
            obj.GetComponent<BreakableTile>().explode = true;
        }
        Destroy(gameObject);
    }

}
