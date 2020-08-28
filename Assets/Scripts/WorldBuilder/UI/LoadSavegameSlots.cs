using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class LoadSavegameSlots : MonoBehaviour
{
    public GameObject loadButtonPrefab;
    public GameObject loadArea;

    private string[] saveFiles;

    public void showLoadScreen()
    {
        GetLoadFiles();
        for (int i = 0; i < saveFiles.Length; i++)
        {
            GameObject buttonObject = Instantiate(loadButtonPrefab);
            buttonObject.transform.SetParent(loadArea.transform,false);

            buttonObject.GetComponent<Button>().onClick.AddListener(()=>
            {
                Grid.worldSaveManager.loadWorld(saveFiles[i]);
            });
            
            buttonObject.GetComponentInChildren<TextMeshProUGUI>().text = saveFiles[i].Replace(
                Application.persistentDataPath + "/saved_worlds"+@"\",""
            ).Replace(".save",""); 
        }
    }

    public void GetLoadFiles()
    {
        string pathFolder = Grid.worldSaveManager.path;
        if (!Directory.Exists(pathFolder))
        {
            Directory.CreateDirectory(pathFolder);
        }

        saveFiles = Directory.GetFiles(pathFolder);
    }
}
