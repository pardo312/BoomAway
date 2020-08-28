using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    
    [HideInInspector]public bool editing;
    [HideInInspector]public string currentLevel;
    void Awake()
    {
        editing=true;
        
        //temporal, cambiar luego
        currentLevel = "LVL1";
    }
    
}
