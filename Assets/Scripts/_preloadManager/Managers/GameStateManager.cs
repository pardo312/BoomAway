using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private int amountGuns = 3;
    //0 = Bomb ; 1 = C4 ; 
    [HideInInspector]public int[] ammo;
    [HideInInspector]public int[] currentAmmo;
    [HideInInspector]public int currentAmmoType = 0;
    [HideInInspector]public bool hasCurrentAmmo = false;
    [HideInInspector]public bool editing;
    [HideInInspector]public string currentLevel;
    private bool endLevel = false;
    
    void Awake()
    {
        setAmmo();
        editing=true;
        //temporal, cambiar luego
        currentLevel = "LVL1";
        ammo = new int[amountGuns];
        currentAmmo = new int[amountGuns];
    }
    void setAmmo(){
        //Bomb
        ammo[0] = 3;
        //C4
        ammo[1] = 4;
        //FastRocket
        ammo[2] = 5;

        //Bomb
        currentAmmo[0] = 3;
        //C4
        currentAmmo[1] = 4;
        //FastRocket
        currentAmmo[2] = 5;
    }
    
}
