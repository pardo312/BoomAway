using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;

public class LoadSavegameSlots : MonoBehaviour
{
    public GameObject loadButtonPrefab;
    public GameObject loadArea;

    private string[] directoryFiles;
    private List<string> saveFiles = new List<string>();

    public void showLoadScreen()
    {
        GetLoadFiles();

        for(int i = 0; i < loadArea.transform.childCount; i++)
        {
            Transform button = loadArea.transform.GetChild(i);
            Destroy(button.gameObject);
        }

        for (int i = 0; i < saveFiles.Count; i++)
        {
            GameObject buttonObject = Instantiate(loadButtonPrefab);
            buttonObject.transform.SetParent(loadArea.transform,false);
            
            int index= i;
            buttonObject.GetComponent<Button>().onClick.AddListener(()=>
            {
                Grid.worldSaveManager.loadWorld(saveFiles[index]);
            });
            
            buttonObject.GetComponentInChildren<TextMeshProUGUI>().text = saveFiles[index].Replace(
                Application.persistentDataPath + "/saved_worlds/",""
            ).Replace(".save",""); 
        }
    }

    public void GetLoadFiles()
    {
        string pathFolder = Grid.worldSaveManager.rootPath+ "/saved_worlds/";
        if (!Directory.Exists(pathFolder))
        {
            Directory.CreateDirectory(pathFolder);
        }

        directoryFiles = Directory.GetFiles(pathFolder);
        for (int i = 0; i < directoryFiles.Length; i++)
        {
            if(!directoryFiles[i].Contains(".state")){
                saveFiles.Add(directoryFiles[i]);
            }
        }
    }
}
