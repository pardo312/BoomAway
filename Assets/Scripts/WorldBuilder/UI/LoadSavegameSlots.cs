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

        for(int i = 0; i < loadArea.transform.childCount; i++)
        {
            Transform button = loadArea.transform.GetChild(i);
            Destroy(button.gameObject);
        }

        for (int i = 0; i < saveFiles.Length; i++)
        {
            GameObject buttonObject = Instantiate(loadButtonPrefab);
            buttonObject.transform.SetParent(loadArea.transform,false);
            
            int index= i;
            buttonObject.GetComponent<Button>().onClick.AddListener(()=>
            {
                Grid.worldSaveManager.loadWorld(saveFiles[index]);
            });
            
            buttonObject.GetComponentInChildren<TextMeshProUGUI>().text = saveFiles[index].Replace(
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
