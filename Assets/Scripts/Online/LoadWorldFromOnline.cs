using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadWorldFromOnline : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         Grid.worldSaveManager.loadWorldFromFirebase(Grid.gameStateManager.currentOnlineLevel,SaveType.Builder);
    }

}
