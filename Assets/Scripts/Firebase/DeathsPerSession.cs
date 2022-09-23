using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class DeathsPerSession : MonoBehaviour
{
    string urlFirebaseAnalytics = "https://boomaway-2ccf0-default-rtdb.firebaseio.com/Analytics/DeathsPerSession.json";

    public void uploadDeaths(int numDeaths)
    {
        #if !UNITY_EDITOR
            string bodyJsonString = "{\"" + "Deaths" + "\":" + numDeaths + "}";
            var request = new UnityWebRequest(urlFirebaseAnalytics+"?auth="+Grid.gameStateManager.tokenFirebase, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SendWebRequest();
        #endif
    }
}
