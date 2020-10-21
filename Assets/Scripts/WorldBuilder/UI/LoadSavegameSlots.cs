﻿using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using SimpleJSON;

public class LoadSavegameSlots : MonoBehaviour
{
    public GameObject loadButtonPrefab;
    public GameObject loadArea;

    private string[] directoryFiles;
    private List<string> saveFiles = new List<string>();

    private string urlFirebaseOnline = "https://boomaway-10de3.firebaseio.com/OnlineLevels/";
    public void showLoadScreen()
    {

        saveFiles=new List<string>();
        for(int i = 0; i < loadArea.transform.childCount; i++)
        {
            Transform button = loadArea.transform.GetChild(i);
            Destroy(button.gameObject);
        }
        
        GetLoadFiles();        
    }
    public void showUserWorldsLoadScreen()
    {

        saveFiles=new List<string>();
        for(int i = 0; i < loadArea.transform.childCount; i++)
        {
            Transform button = loadArea.transform.GetChild(i);
            Destroy(button.gameObject);
        }
        
        GetUserWorldsLoadFiles();        
    }
    public void GetLoadFiles()
    {
        StartCoroutine(UnityRequestLevelOnline());
    }
    public void GetUserWorldsLoadFiles()
    {
        StartCoroutine(UnityRequestUserLevelsOnline());
    }

    IEnumerator UnityRequestLevelOnline()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(urlFirebaseOnline + ".json"))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                JSONNode data = JSON.Parse(webRequest.downloadHandler.text);
                foreach(JSONNode player in data)
                {
                    saveFiles.Add(player["LevelName"]);
                }
            }
        }
        for (int i = 0; i < saveFiles.Count; i++)
        {
            GameObject buttonObject = Instantiate(loadButtonPrefab);
            buttonObject.transform.SetParent(loadArea.transform,false);
            LoadScene loadSceneScript = buttonObject.AddComponent<LoadScene>();
            
            buttonObject.GetComponent<Button>().onClick.AddListener(()=>
            {   
                loadSceneScript.loadScene("OnlineLevel");
                Grid.gameStateManager.currentOnlineLevel = saveFiles[i];
            });
            buttonObject.GetComponentInChildren<TextMeshProUGUI>().text = saveFiles[i];
        }
        
    }

    IEnumerator UnityRequestUserLevelsOnline()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(urlFirebaseOnline + ".json"))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                JSONNode data = JSON.Parse(webRequest.downloadHandler.text);
                foreach(JSONNode player in data)
                {
                    if(player["user"].Equals(Grid.gameStateManager.usernameOnline))
                        saveFiles.Add(player["LevelName"]);
                }
            }
        }
        for (int i = 0; i < saveFiles.Count; i++)
        {
            GameObject buttonObject = Instantiate(loadButtonPrefab);
            buttonObject.transform.SetParent(loadArea.transform,false);
            LoadScene loadSceneScript = buttonObject.AddComponent<LoadScene>();
            
            buttonObject.GetComponent<Button>().onClick.AddListener(()=>
            {   
                loadSceneScript.loadScene("OnlineLevel");
                Grid.gameStateManager.currentOnlineLevel = saveFiles[i];
            });
            buttonObject.GetComponentInChildren<TextMeshProUGUI>().text = saveFiles[i];
        }
        
    }
}
