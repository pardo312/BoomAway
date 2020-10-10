using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    //Ammo
    [HideInInspector]public int[] ammo;
    [HideInInspector]public int[] currentAmmo;
    [HideInInspector]public int currentAmmoType ;
    [HideInInspector]public bool hasCurrentAmmo;

    //bools
    [HideInInspector]public bool editing;
    [HideInInspector]public bool IsPaused;
    [HideInInspector]public bool IsEndLevel;
    [HideInInspector]public bool IsDead;
    [HideInInspector]public bool IsOnGame;
    [HideInInspector]public bool IsOnStoryMode;

    //StateOfGame
    [HideInInspector]public string currentLevel;
    [HideInInspector]public string currentWorldBuilderLevel;
    [HideInInspector]public float health;

    //Analytics
    [HideInInspector] public int currentDeaths;
    [SerializeField] private LastLevelPlayed lastLevel;
    [SerializeField] private DeathsPerSession deaths;
    public LevelsPlayed frequency;
    
    void Awake()
    {
        IsOnGame=false;
        setAmmo();
        initVariables();
    }
    void setAmmo(){
        ammo = new int[Constants.AMOUNT_GUNS];
        currentAmmo = new int[Constants.AMOUNT_GUNS];
        ammo[Constants.BOMB_TYPE] = 3;
        ammo[Constants.C4_TYPE] = 4;
        ammo[Constants.FAST_ROCKET_TYPE] = 5;
        ammo[Constants.SLOW_ROCKET_TYPE] = 5;

        currentAmmo[Constants.BOMB_TYPE] = 3;
        currentAmmo[Constants.C4_TYPE] = 4;
        currentAmmo[Constants.FAST_ROCKET_TYPE] = 5;
        currentAmmo[Constants.SLOW_ROCKET_TYPE] = 5;
    }
    public void initVariables()
    { 
        //temporal, cambiar luego
        currentLevel = "LVL2";
        currentWorldBuilderLevel = "";
        currentAmmoType = Constants.BOMB_TYPE;
        hasCurrentAmmo = false;
        editing=false;
        IsPaused = false;
        IsDead = false;
        IsEndLevel = false;
        IsOnStoryMode = false;
        health= 1;
    }

    public void initRestart()
    {
         ammo[Constants.BOMB_TYPE] = 3;
        ammo[Constants.C4_TYPE] = 4;
        ammo[Constants.FAST_ROCKET_TYPE] = 5;
        ammo[Constants.SLOW_ROCKET_TYPE] = 5;

        //temporal, cambiar luego
        currentAmmo[Constants.BOMB_TYPE] =  ammo[Constants.BOMB_TYPE];
        currentAmmo[Constants.C4_TYPE] =  ammo[Constants.C4_TYPE];
        currentAmmo[Constants.FAST_ROCKET_TYPE] =  ammo[Constants.FAST_ROCKET_TYPE];
        currentAmmo[Constants.SLOW_ROCKET_TYPE] =  ammo[Constants.SLOW_ROCKET_TYPE];
        currentAmmoType = Constants.BOMB_TYPE;
        hasCurrentAmmo = false;
        editing = false;
        IsPaused = false;
        IsDead = false;
        IsEndLevel = false;
        IsOnStoryMode = false;
        health= 1;
    }
    public bool canPause()
    {
        return !(IsDead || IsEndLevel || !IsOnGame);
    }

    private void OnApplicationQuit()
    {
        lastLevel.uploadLastLevel(currentLevel);
        deaths.uploadDeaths(currentDeaths);
        frequency.uploadLevelFrequency();
    }

}
