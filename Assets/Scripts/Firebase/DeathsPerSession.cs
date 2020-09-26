using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class DeathsPerSession : MonoBehaviour
{
    string urlFirebaseAnalytics = "https://boomaway-10de3.firebaseio.com/Analytics/DeathsPerSession";

    //TODO  Aumentar el número de muertes cada vez que el personaje se cae del mundo
    //      Para esto, primero hay que implementar la mecánica de muerte (i.e. un reinicio forzado al tocar
    //      un trigger debajo del nivel

    public void uploadDeaths(int numDeaths)
    {
        string bodyJsonString = "{\"" + "Deaths" + "\":\"" + numDeaths + "\"}";
        var request = new UnityWebRequest(urlFirebaseAnalytics, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SendWebRequest();
    }
}
