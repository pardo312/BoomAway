using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    
    [HideInInspector]public bool editing;
    void Awake()
    {
        editing=true;
    }
    
}
