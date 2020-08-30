using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelState : MonoBehaviour
{
    //0 = Bomb ; 1 = C4 ; 
    [HideInInspector]public int[] ammo = new int[2];
    private bool endLevel = false;
    
    private void Start() {
        ammo[0] = 3;
        ammo[1] = 4;
    }
}
