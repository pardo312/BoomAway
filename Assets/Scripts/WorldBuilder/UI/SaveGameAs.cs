using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveGameAs : MonoBehaviour
{
    public TMP_InputField saveName;

    public void saveGameAs(){
        Grid.worldSaveManager.saveWorld(saveName.text);
    }
}
