using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class CreateComment : MonoBehaviour
{
    public TMP_InputField comentario;
    private string urlFirebaseOnline = "https://boomaway-10de3.firebaseio.com/OnlineLevels";



    public void enviarComentario()
    {
        StartCoroutine(sendComment(comentario.text));

    }

    IEnumerator sendComment(string level)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Post(urlFirebaseOnline + '/' + level + '/' + "Comments" + ".json", "Comment: "+ '"' + comentario.text + '"' ))
        {
            yield return webRequest.SendWebRequest();
        }
    }

}
