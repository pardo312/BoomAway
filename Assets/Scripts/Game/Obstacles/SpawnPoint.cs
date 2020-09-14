using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        bool editing = Grid.gameStateManager.editing;
        if (!editing)
        {
            player.transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }
    }

}
