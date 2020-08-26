using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoomAway.Assets.Scripts.PreloadManager;
static class Grid
{
    public static GameSaveManager gameSaveManager;

    static Grid()
    {
        GameObject g = safeFind("Managers");
        gameSaveManager = (GameSaveManager)SafeComponent(g, "GameSaveManager");
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