using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CreateComment : MonoBehaviour
{
    public TMP_InputField comentario;
    private string urlFirebaseOnline = "https://boomaway-10de3.firebaseio.com/OnlineLevels";
    private string lvl = "";
    public LoadSavegameSlots loadSavegame; 

    public void enviarComentario()
    {
        StartCoroutine(sendComment(comentario.text));
    }

    public void updateLevel(string level)
    {
        lvl = level;
    }

    IEnumerator sendComment(string level)
    {
        Debug.Log(urlFirebaseOnline + '/' + lvl + '/' + "Comments" + ".json");
        Debug.Log("Comment: " + '"' + comentario.text + '"');
        using (UnityWebRequest webRequest = new UnityWebRequest(urlFirebaseOnline + '/' + lvl + '/' + "Comments.json?auth="+Grid.gameStateManager.tokenFirebase, "POST"))
        {
            Debug.Log(urlFirebaseOnline + '/' + lvl + '/' + "Comments.json");
            byte[] bodyRaw = Encoding.UTF8.GetBytes('"' + comentario.text + '"');
            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            yield return webRequest.SendWebRequest();
            Debug.Log(webRequest.result);
            if (webRequest.isNetworkError)
            {
                Debug.LogError(webRequest.responseCode);
            }
            else
            {
                Debug.Log("No errors");
                loadSavegame.showCommentScreen(lvl);
            }
        }
    }

}
