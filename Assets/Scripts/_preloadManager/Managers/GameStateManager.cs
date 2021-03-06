﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class GameStateManager : MonoBehaviour
{
    
    [HideInInspector]public string usernameOnline ;
    //Ammo
    [HideInInspector]public int[] ammo;
    [HideInInspector]public int[] currentAmmo;
    [HideInInspector]public int currentAmmoType ;
    [HideInInspector]public int currentBoxAmmoType;
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
    [HideInInspector] public string currentOnlineLevel;
    [HideInInspector] public string currentOnlineLevelForPost;
    [HideInInspector]public string currentWorldBuilderLevel;
    [HideInInspector]public float health;
    [HideInInspector]public bool damaged;
    [HideInInspector]public bool levelLoaded = true;

    //Analytics
    [HideInInspector] public int currentDeaths;
    [SerializeField] private LastLevelPlayed lastLevel;
    [SerializeField] private DeathsPerSession deaths;
    [SerializeField] public List<Sprite> ammoTypeSprites;
    public LevelsPlayed frequency;
    public string tokenFirebase = "";
    private int tokenExpirationTime =3600;
     private float initTimeTokenFirebase;
    //Points
    public float points;

    private void Update() {
        getTokenFirebase();
        if(Time.time >= initTimeTokenFirebase + tokenExpirationTime)
        {
            tokenFirebase= "";
            Debug.Log("Reset Firebase Token");
            initTimeTokenFirebase = Time.time;
        }
    }
    void Awake()
    {
        initTimeTokenFirebase = Time.time;
        getTokenFirebase();
        usernameOnline = "Anonymous";
        IsOnGame=false;
        setAmmo();
        initVariables();
    }
    public void getTokenFirebase()
    {
        if(tokenFirebase== ""){
            StartCoroutine(requestTokenFirebase());
        }
    }
    IEnumerator requestTokenFirebase()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("https://boomaway-ps.netlify.app/.netlify/functions/api"))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                tokenFirebase = webRequest.downloadHandler.text;
            }
        }
    }
    void setAmmo(){
        currentBoxAmmoType= 0;

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
        currentOnlineLevel = "";
        currentWorldBuilderLevel="";
        currentAmmoType = Constants.BOMB_TYPE;
        hasCurrentAmmo = false;
        editing=false;
        IsPaused = false;
        IsDead = false;
        IsEndLevel = false;
        IsOnStoryMode = false;
        health= 1;
        points = 999f;
        damaged = false;
    }

    public void initRestart()
    {
        hasCurrentAmmo = false;
        editing = false;
        IsPaused = false;
        IsDead = false;
        IsEndLevel = false;
        IsOnStoryMode = false;
        health= 1;
        points = 999f;
        damaged = false;
    }
    public void initRestartTutorial()
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
        points = 999f;
        damaged = false;
    }
    public bool canPause()
    {
        return !(IsDead || IsEndLevel || !IsOnGame);
    }

    private void OnApplicationFocus(bool isFocused)
    {
        if(!isFocused)
        {
            lastLevel.uploadLastLevel(currentLevel);
            deaths.uploadDeaths(currentDeaths);
            frequency.uploadLevelFrequency();
        }
    }
    private void OnApplicationQuit()
    {
        lastLevel.uploadLastLevel(currentLevel);
        deaths.uploadDeaths(currentDeaths);
        frequency.uploadLevelFrequency();
    }

    //Function called from the html template to send analytics even if browser tab/window is closed
    public void OnBrowserClose()
    {
        lastLevel.uploadLastLevel(currentLevel);
        deaths.uploadDeaths(currentDeaths);
        frequency.uploadLevelFrequency();
    }

}
