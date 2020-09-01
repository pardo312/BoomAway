using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [HideInInspector]public int[] ammo;
    [HideInInspector]public int[] currentAmmo;
    [HideInInspector]public int currentAmmoType ;
    [HideInInspector]public bool hasCurrentAmmo = false;
    [HideInInspector]public bool editing;
    [HideInInspector]public string currentLevel;
    private bool endLevel = false;
    
    void Awake()
    {
        currentAmmoType = Constants.BOMB_TYPE;

        ammo = new int[Constants.AMOUNT_GUNS];
        currentAmmo = new int[Constants.AMOUNT_GUNS];
        setAmmo();

        editing=true;
        //temporal, cambiar luego
        currentLevel = "LVL1";
    }
    void setAmmo(){

        ammo[Constants.BOMB_TYPE] = 3;
        ammo[Constants.C4_TYPE] = 4;
        ammo[Constants.FAST_ROCKET_TYPE] = 5;
        ammo[Constants.SLOW_ROCKET_TYPE] = 5;

        ammo[Constants.BOMB_TYPE] = 3;
        ammo[Constants.C4_TYPE] = 4;
        ammo[Constants.FAST_ROCKET_TYPE] = 5;
        ammo[Constants.SLOW_ROCKET_TYPE] = 5;
    }
    
}
