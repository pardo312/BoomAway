using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System.Text;

public class WorldBuilderUsedTimes : MonoBehaviour
{
    [SerializeField]private LoadScene loadSceneScript;
    string urlFirebaseAnalytics = "https://boomaway-10de3.firebaseio.com/Analytics/WorldBuilder.json";
    int currentUsedTimes = 0;
    private void Awake() {
        GetWorldBuilderUsedTimesMethod();
    }
    public void UploadWorldBuilderUsedTimesMethod(){
        StartCoroutine(UploadWorldBuilderUsedTimes());
    }
    IEnumerator UploadWorldBuilderUsedTimes()
    {
        
        string doubleQuotation  = ('"' + "" );
        string bodyJsonString ="{"+doubleQuotation+"usedTimes"+doubleQuotation+":"+ (currentUsedTimes+1) + "}";
        Debug.Log(bodyJsonString);
        var request = new UnityWebRequest(urlFirebaseAnalytics, "PUT");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        loadSceneScript.loadScene("WorldBuilder"); 
    }

    private void GetWorldBuilderUsedTimesMethod(){
        StartCoroutine(GetWorldBuilderUsedTimes());
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
                Debug.Log(currentUsedTimes);           
            }
        }
    }
}
