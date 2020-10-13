using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System.Text;

public class TimeOnLevel : MonoBehaviour
{
    string urlFirebaseAnalytics = "https://boomaway-10de3.firebaseio.com/Analytics/TimeOnLevel/.json";
    private float timer = 0f;
    void Update(){
         timer += Time.deltaTime;
     }
    public void uploadLevelCompletionTime()
    {
        #if !UNITY_EDITOR
            //Double Quotation
            string dQ  = ('"' + "" );

            string bodyJsonString ="{"+dQ+ Grid.gameStateManager.currentLevel + dQ +":"+ (int) timer + "}";
            var request = new UnityWebRequest(urlFirebaseAnalytics, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
            request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SendWebRequest();
        #endif
    }
}
