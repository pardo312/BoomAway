using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class LeaderBoardScript : MonoBehaviour
{
    public string level;
    public GameObject LeaderBoard;
    public GameObject ScorePrefab;
    public GameObject VerticalListArea;
    private List<Tuple<string, int>> listaScores;

    private string urlFirebaseOnline = "https://boomaway-10de3.firebaseio.com/StoryLevels/";
    private string urlFirebaseOnlineLvls = "https://boomaway-10de3.firebaseio.com/OnlineLevels/";

    public void showLoadScreen()
    {
        listaScores = new List<Tuple<string, int>>();
        for (int i = 0; i < VerticalListArea.transform.childCount; i++)
        {
            Transform button = VerticalListArea.transform.GetChild(i);
            Destroy(button.gameObject);
        }
        loadLeaderBoard();
    }

    public void loadLeaderBoard()
    {
        StartCoroutine(RequestLeaderBoard());
    }


    IEnumerator RequestLeaderBoard()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(urlFirebaseOnline + level + "/Leaderboard.json"))
        {
            Debug.Log(urlFirebaseOnline + level + "/Leaderboard.json");
            yield return request.SendWebRequest();
            if (request.isNetworkError)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                Debug.Log(request.result);

                if (!request.downloadHandler.text.Equals("null"))
                {

                    JSONNode data = JSON.Parse(request.downloadHandler.text);
                    foreach (KeyValuePair<string, JSONNode> kvp in (JSONObject)data)
                    {
                        Debug.Log(kvp.Key);
                        foreach (JSONNode score in kvp.Value)
                        {
                            Debug.Log(score);
                            listaScores.Add(new Tuple<string, int>(kvp.Key, (int)score));
                        }
                    }
                }
            }
        }
        if (listaScores.Count > 0)
        {
            listaScores.Sort((a, b) => b.Item2.CompareTo(a.Item2));
        }
        instantiateScores();
    }

    public void showOnlineLoadScreen()
    {
        listaScores = new List<Tuple<string, int>>();
        for (int i = 0; i < VerticalListArea.transform.childCount; i++)
        {
            Transform button = VerticalListArea.transform.GetChild(i);
            Destroy(button.gameObject);
        }
        loadLeaderBoardOnline();
    }

    public void loadLeaderBoardOnline()
    {
        StartCoroutine(RequestLeaderBoardOnline());
    }

    IEnumerator RequestLeaderBoardOnline()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(urlFirebaseOnlineLvls + level + "/Leaderboard.json"))
        {
            Debug.Log(urlFirebaseOnlineLvls + level + "/Leaderboard.json");
            yield return request.SendWebRequest();
            if (request.isNetworkError)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                Debug.Log(request.result);
                if (!request.downloadHandler.text.Equals("null"))
                {
                    JSONNode data = JSON.Parse(request.downloadHandler.text);

                    foreach (KeyValuePair<string, JSONNode> kvp in (JSONObject)data)
                    {
                        Debug.Log(kvp.Key);
                        foreach (JSONNode score in kvp.Value)
                        {
                            Debug.Log(score);
                            listaScores.Add(new Tuple<string, int>(kvp.Key, (int)score));
                        }
                    }
                }
            }
        }
        if (listaScores.Count > 0)
        {
            listaScores.Sort((a, b) => b.Item2.CompareTo(a.Item2));
        }
        instantiateScores();
    }

    public void instantiateScores()
    {
        for (int i = 0; i < listaScores.Count; i++)
        {
            GameObject buttonObject = Instantiate(ScorePrefab);
            buttonObject.transform.SetParent(VerticalListArea.transform, false);
            buttonObject.transform.GetChild(0).GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "" + listaScores[i].Item2;
            buttonObject.transform.GetChild(1).GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "" + listaScores[i].Item1;
            buttonObject.transform.GetChild(2).GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "" + (i + 1);
        }
        LeaderBoard.SetActive(true);
    }
}



