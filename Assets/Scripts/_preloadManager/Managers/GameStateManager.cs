using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    //0 = Bomb ; 1 = C4 ; 
    [HideInInspector]public int[] ammo = new int[2];
    [HideInInspector]public bool editing;
    [HideInInspector]public string currentLevel;
    private bool endLevel = false;
    
    void Awake()
    {
        setAmmo();
        editing=true;
        //temporal, cambiar luego
        currentLevel = "LVL1";
    }
    void setAmmo(){
        //Bomb
        ammo[0] = 3;
        //C4
        ammo[1] = 4;
    }
    
}
