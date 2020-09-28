﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableTile : MonoBehaviour
{

    public bool explode;
    public LayerMask layerToExplode;

    private void FixedUpdate()
    {
        if (explode)
        {
            Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, 2, layerToExplode);
            StartCoroutine(ExecuteAfterTime(0.5f, objects));
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
