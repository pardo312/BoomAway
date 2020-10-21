using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine.Networking;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Login : MonoBehaviour
{
    [SerializeField]private TMP_InputField userTextField;
    [SerializeField]private TMP_InputField passwordTextField;
    [SerializeField]private GameObject onlineMenu;
    [SerializeField]private TextMeshProUGUI failedLoginMessage;
    private string urlFirebaseOnline = "https://boomaway-10de3.firebaseio.com/Users/";

    public void loginOnline()
    {
        StartCoroutine(UnityRequestUsers());
    }
    
    IEnumerator UnityRequestUsers()
    {
        List<string[]> users = new List<string[]>();
        using (UnityWebRequest webRequest = UnityWebRequest.Get(urlFirebaseOnline + ".json"))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                JSONNode data = JSON.Parse(webRequest.downloadHandler.text);
                foreach(JSONNode player in data)
                {
                    string[] user = new string[2];
                    user[0] = player["user"];
                    user[1] = player["password"];
                    users.Add(user);
                }
            }
        }
        bool found = false;
        for (int i = 0; i < users.Count; i++)
        {
            //Deserializado de password
            string password = users[i][1];
            byte[] bytesPassword = System.Convert.FromBase64String(password);
            string psw = Deserialize<string>(bytesPassword);

            if(users[i][0].Equals(userTextField.text) && psw.Equals(passwordTextField.text)){
                onlineMenu.SetActive(true);
                transform.parent.gameObject.SetActive(false);
                found = true;
                Grid.gameStateManager.usernameOnline = userTextField.text;
            }
        }
        if(!found){
            failedLoginMessage.SetText("usuario o contraseña incorrecta");
            StartCoroutine(disableFailText());
        }
    }
    private T Deserialize<T>(byte[] param)
    {
        using (MemoryStream ms = new MemoryStream(param))
        {
            BinaryFormatter br = new BinaryFormatter();
            return (T)br.Deserialize(ms);
        }
    }
    IEnumerator disableFailText()
    {
        yield return new WaitForSeconds(3);
        failedLoginMessage.SetText("");
    }
}
