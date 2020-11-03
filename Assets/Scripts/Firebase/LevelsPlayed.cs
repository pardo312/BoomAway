using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System.Text;

public class LevelsPlayed : MonoBehaviour
{
    string urlFirebaseAnalytics;

    Dictionary<string, int> lvlFrequency;
    [SerializeField] private int numberOfLevels = 8;

    private void Awake()
    {
        urlFirebaseAnalytics = "https://boomaway-10de3.firebaseio.com/Analytics/LevelFrequency.json";
        lvlFrequency = new Dictionary<string, int>();

        //Populate the dictionary, one entry per level
        for (int i = 1; i <= numberOfLevels; i++)
            lvlFrequency.Add("LVL" + i.ToString(), 0);
    }

    public void addToLVL(string lvlToAdd)
    {
        lvlFrequency[lvlToAdd]++;
    }

    public void uploadLevelFrequency()
    {
        #if !UNITY_EDITOR
            string bodyJsonString = "{";

            foreach(string key in lvlFrequency.Keys)
            {
                if(!key.Equals("LVL" + numberOfLevels.ToString()))
                {
                    bodyJsonString += "\"" + key + "\": " + lvlFrequency[key].ToString() + ",";
                }
                else
                {
                    bodyJsonString += "\"" + key + "\": " + lvlFrequency[key].ToString();       //No se le pone "," al último valor en la lista
                }
            }

            bodyJsonString += "}";

            var request = new UnityWebRequest(urlFirebaseAnalytics+"?auth="+Grid.gameStateManager.tokenFirebase, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SendWebRequest();
        #endif
    }
}
