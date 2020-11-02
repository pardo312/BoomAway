using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CreateComment : MonoBehaviour
{
    public TMP_InputField comentario;
    private string urlFirebaseOnline = "https://boomaway-10de3.firebaseio.com/OnlineLevels";
    public TextMeshProUGUI comm;
    public string temp; 


    public void enviarComentario()
    {
        comentario = gameObject.GetComponent<TMP_InputField>();
        comm = gameObject.GetComponent<TextMeshProUGUI>();
        StartCoroutine(sendComment(temp));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void change(string temp1)
    {
        temp = temp1;
    }

    IEnumerator sendComment(string level)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Post(urlFirebaseOnline + '/' + level + '/' + "Comments" + ".json", "Comment: "+ '"' +  + '"' ))
        {
            yield return webRequest.SendWebRequest();
        }
    }

}
