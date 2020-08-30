using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelState : MonoBehaviour
{
    private bool enterPlay = true;
    [SerializeField] private GameObject[] gunsPrefab;
    [SerializeField] private Transform ammoParent;
    private void Update()
    {
        if (!Grid.gameStateManager.editing)
        {
            if (enterPlay)
            {
                int[] ammoGuns = Grid.gameStateManager.ammo;
                for (int i = 0; i < ammoGuns.Length; i++)
                {
                    for (int j = 0; j < ammoGuns[i]; j++)
                    {
                        Instantiate(gunsPrefab[i], ammoParent);
                    }
                }
                enterPlay = false;
            }
        }
        else
        {
            if (!enterPlay)
                enterPlay = true;
        }

    }
}
