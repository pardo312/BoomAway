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
        currentLevel = "LVL1";
        currentWorldBuilderLevel = "";
        currentAmmoType = Constants.BOMB_TYPE;
        hasCurrentAmmo = false;
        editing=false;
        IsPaused = false;
        IsDead = false;
        IsEndLevel = false;
        IsOnStoryMode = false;
        currentDeaths = 0;
        health= 1;
    }

    public void initRestart()
    {
        //temporal, cambiar luego
        currentAmmoType = Constants.BOMB_TYPE;
        hasCurrentAmmo = false;
        editing = false;
        IsPaused = false;
        IsDead = false;
        IsEndLevel = false;
        IsOnStoryMode = false;
        currentDeaths = 0;
        health= 1;
    }
    public bool canPause()
    {
        return !(IsDead || IsEndLevel || !IsOnGame);
    }

    //TODO  Aumentar el número de muertes cada vez que el personaje se cae del mundo
    //      Para esto, primero hay que implementar la mecánica de muerte (i.e. un reinicio forzado al tocar
    //      un trigger debajo del nivel

    private void OnApplicationQuit()
    {
        lastLevel.uploadLastLevel(currentLevel);
        deaths.uploadDeaths(currentDeaths);
    }

}
