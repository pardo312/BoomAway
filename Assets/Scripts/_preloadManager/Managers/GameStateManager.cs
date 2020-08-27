using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    
    public bool editing;
    void Awake()
    {
        editing=true;
    }
    public void changeEditorMode()
    {
        editing = !editing;
    }
}
