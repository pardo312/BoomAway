using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessToManagersForButtons : MonoBehaviour
{
    public void saveGame(string path, object data) {
        Grid.gameSaveManager.saveGame(path,data);
    }
}
