using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoomAway.Assets.Scripts.PreloadManager;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.SceneManagement;
#endif

static class Grid
{
    #if UNITY_EDITOR
    public static NextSceneLoader nextSceneLoader;
    #endif
    public static GameSaveManager gameSaveManager;
   
    static Grid()
    {
        GameObject g = safeFind("Managers");
        gameSaveManager = (GameSaveManager)SafeComponent(g, "GameSaveManager");

        #if UNITY_EDITOR
        nextSceneLoader = (NextSceneLoader)SafeComponent(g, "NextSceneLoader");
        SceneManager.LoadScene(System.IO.Path.GetFileNameWithoutExtension(EditorPrefs.GetString("SceneAutoLoader.PreviousScene")));
        #endif
    }
    private static GameObject safeFind(string s)
    {
        GameObject g = GameObject.Find(s);
        return g;
    }
    private static Component SafeComponent(GameObject g, string s)
    {
        Component c = g.GetComponent(s);
        return c;
    }
   

}