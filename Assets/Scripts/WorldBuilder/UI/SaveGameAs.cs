using UnityEngine;
using TMPro;

public class SaveGameAs : MonoBehaviour
{
    public TMP_InputField saveName;

    public void saveGameAs(){

        var savemanager = Grid.worldSaveManager;
        savemanager.saveWorld(saveName.text,SaveType.Builder);
    }
}
