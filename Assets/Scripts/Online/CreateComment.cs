using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class CreateComment : MonoBehaviour
{
    public TMP_InputField comentario;
    private string urlFirebaseOnline = "https://boomaway-10de3.firebaseio.com/OnlineLevels";



    void enviarComentario(string level)
    {
        StartCoroutine(sendComment(level));

    }

    IEnumerator sendComment(string level)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Post(urlFirebaseOnline + '/' + level + '/' + "Comments" + ".json", comentario.text))
        {
            yield return webRequest.SendWebRequest();
        }
    }

}
