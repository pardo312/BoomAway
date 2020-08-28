using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelFile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Grid.worldSaveManager.loadWorldFromFolder(Grid.gameStateManager.currentLevel,SaveType.Story);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
