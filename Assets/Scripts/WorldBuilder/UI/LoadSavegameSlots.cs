using UnityEngine;
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
    public GameObject commentPrefab;
    public GameObject loadCommentArea;

    private string[] directoryFiles;
    private List<string> saveFiles = new List<string>();
    private List<string> savedComments = new List<string>();
    private string tempLevel;

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

    public void showCommentScreen()
    {
        savedComments = new List<string>();
        for (int i = 0; i < loadCommentArea.transform.childCount; i++)
        {
            Transform button = loadCommentArea.transform.GetChild(i);
            Destroy(button.gameObject);
        }

        GetLoadComments();
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

    public void GetLoadComments()
    {
        StartCoroutine(UnityRequestCommentsOnline());
    }

    IEnumerator UnityRequestCommentsOnline() {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(urlFirebaseOnline + Grid.gameStateManager.currentLevel + ".json"))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                JSONNode data = JSON.Parse(webRequest.downloadHandler.text);
                foreach (JSONNode player in data)
                {
                    saveFiles.Add(player["LevelName"]);
                }
            }
        }
        for (int i = 0; i < saveFiles.Count; i++)
        {
            GameObject buttonObject = Instantiate(commentPrefab);
            buttonObject.transform.SetParent(loadCommentArea.transform, false);
            buttonObject.GetComponentInChildren<TextMeshProUGUI>().text = saveFiles[i];
        }
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
            int index = i;
            buttonObject.GetComponent<Button>().onClick.AddListener(()=>
            {   
                Grid.gameStateManager.currentOnlineLevel = saveFiles[index];
                loadSceneScript.loadScene("OnlineLevel");
            });
            buttonObject.GetComponentInChildren<TextMeshProUGUI>().text = saveFiles[index];
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
            
            int index = i;
            buttonObject.GetComponent<Button>().onClick.AddListener(()=>
            {   
                //Deshabilita el boton de back
                
                GameObject.Find("BackButtonLoadWorld").SetActive(false);
                Grid.gameStateManager.currentWorldBuilderLevel = saveFiles[index];
                Time.timeScale=1;
                loadSceneScript.loadScene("WorldBuilder");
            });
            buttonObject.GetComponentInChildren<TextMeshProUGUI>().text = saveFiles[index];
        }
        
    }
}
