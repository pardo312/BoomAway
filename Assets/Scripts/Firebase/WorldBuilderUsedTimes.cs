using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System.Text;

public class WorldBuilderUsedTimes : MonoBehaviour
{
    [SerializeField]private LoadScene loadSceneScript;
    string urlFirebaseAnalytics = "https://boomaway-2ccf0-default-rtdb.firebaseio.com/Analytics/WorldBuilder.json";
    int currentUsedTimes = 0;
    private void Awake() {
        GetWorldBuilderUsedTimesMethod();
    }
    public void UploadWorldBuilderUsedTimesMethod(){
        #if !UNITY_EDITOR
            StartCoroutine(UploadWorldBuilderUsedTimes());
        #endif
    }
    IEnumerator UploadWorldBuilderUsedTimes()
    {
        string doubleQuotation  = ('"' + "" );
        string bodyJsonString ="{"+doubleQuotation+"usedTimes"+doubleQuotation+":"+ (currentUsedTimes+1) + "}";
        var request = new UnityWebRequest(urlFirebaseAnalytics+ "?auth="+Grid.gameStateManager.tokenFirebase, "PUT");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        loadSceneScript.loadScene("WorldBuilder"); 
    }

    private void GetWorldBuilderUsedTimesMethod(){
        #if !UNITY_EDITOR
            StartCoroutine(GetWorldBuilderUsedTimes());
        #endif
    }
    IEnumerator GetWorldBuilderUsedTimes()
    {
       using (UnityWebRequest webRequest = UnityWebRequest.Get(urlFirebaseAnalytics))
        {    
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                JSONNode data = JSON.Parse(webRequest.downloadHandler.text);
                currentUsedTimes=(int)data["usedTimes"];          
            }
        }
    }
}
