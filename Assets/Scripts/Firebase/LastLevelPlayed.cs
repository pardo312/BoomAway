using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class LastLevelPlayed : MonoBehaviour
{
    string urlFirebaseAnalytics = "https://boomaway-10de3.firebaseio.com/Analytics/LastLevel.json";

    public void uploadLastLevel(string lvl)
    {
        //Double Quotation
        string dQ = ('"' + "");

        string bodyJsonString = "{" + dQ + "LVL" + dQ + ":" + dQ + lvl + dQ + "}";
        Debug.Log(bodyJsonString);
        var request = new UnityWebRequest(urlFirebaseAnalytics, "PUT");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SendWebRequest();
    }
}
