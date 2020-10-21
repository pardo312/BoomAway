using System.IO;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using TMPro;

public class SignUp : MonoBehaviour
{
    [SerializeField]private TMP_InputField userTextField;
    [SerializeField]private TMP_InputField passwordTextField;
    [SerializeField]private GameObject onlineMenu;
    [SerializeField]private TextMeshProUGUI failSingupText;
    private string urlFirebaseOnline = "https://boomaway-10de3.firebaseio.com/Users/";

    // Update is called once per frame
    public void SignUpRequest()
    {
        if(userTextField.text != "" && passwordTextField.text != "")
            StartCoroutine(UnityRequestSingUp());
        else
        {
            failSingupText.text = "no puedes registrate con campos vacios";
            StartCoroutine(disableFailText());
        }
    }
    IEnumerator UnityRequestSingUp(){

        byte[] bytesPassword = Serialize(passwordTextField.text);
        string dataPassword = System.Convert.ToBase64String(bytesPassword);

        string dq = ('"' + "");
        string bodyJsonString = "{" + dq + "user" + dq + ":" + dq + (userTextField.text) + dq + "," + dq + "password" + dq + ":" + dq + (dataPassword) + dq + "}";

        var request = new UnityWebRequest(urlFirebaseOnline + ".json", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        onlineMenu.SetActive(true);
        transform.parent.gameObject.SetActive(false);
        Grid.gameStateManager.usernameOnline = userTextField.text;
    }
    private byte[] Serialize<T>(T obj)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            BinaryFormatter br = new BinaryFormatter();
            br.Serialize(ms, obj);
            ms.Position = 0;
            byte[] content = ms.GetBuffer();
            return content;
        }
    }
    IEnumerator disableFailText()
    {
        yield return new WaitForSeconds(3);
        failSingupText.SetText("");
    }
}
