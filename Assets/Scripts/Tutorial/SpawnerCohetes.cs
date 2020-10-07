using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCohetes : MonoBehaviour
{

    public GameObject object_to_spawn;
    public float Timer = 3;
    GameObject rocket_clone;

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0f)
        {
            rocket_clone = Instantiate(object_to_spawn, gameObject.transform.position, transform.rotation) as GameObject;
            Timer = 3f;
        }
    }
}
