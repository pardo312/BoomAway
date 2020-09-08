using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableTile : MonoBehaviour
{

    public bool explode;

    private void Update()
    {
        if (explode)
        {
            GameObject.Destroy(gameObject);
        }
    }

}
