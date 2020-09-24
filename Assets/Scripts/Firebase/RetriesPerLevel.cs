using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class RetriesPerLevel : MonoBehaviour
{
    string urlFirebaseAnalytics = "https://boomaway-10de3.firebaseio.com/Analytics/RetriesPerLevel";
    int currentRetries = 0;
    private void Awake()
    {
        GetRetriesOfLevelMethod();
    }
    public void UploadRetriesMethod()
    {
        StartCoroutine(UploadRetries());
    }
    IEnumerator UploadRetries()
    {
        var lvl = Grid.gameStateManager.currentLevel;
        string doubleQuotation = ('"' + "");
        string bodyJsonString = "{" + doubleQuotation + "retries" + doubleQuotation + ":" + (currentRetries + 1) + "}";
        var request = new UnityWebRequest(urlFirebaseAnalytics + "/" + lvl + ".json", "PUT");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
    }

    private void GetRetriesOfLevelMethod()
    {
        StartCoroutine(GetRetriesOfLevel());
    }
    IEnumerator GetRetriesOfLevel()
    {
        var lvl = Grid.gameStateManager.currentLevel;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(urlFirebaseAnalytics + "/" + lvl + ".json"))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                JSONNode data = JSON.Parse(webRequest.downloadHandler.text);
                currentRetries = (int)data["retries"];
            }
        }
    }
}
