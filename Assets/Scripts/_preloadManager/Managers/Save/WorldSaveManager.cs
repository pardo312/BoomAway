﻿using System.IO;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;


namespace BoomAway.Assets.Scripts.PreloadManager
{
    public class WorldSaveManager : MonoBehaviour
    {
        public MakerTile[] makerTilePrefab;
        MakerTile[] makerTiles;
        [HideInInspector] public string rootPath;
        private string queryResultSAVE = "";
        private string queryResultSTATE = "";


        private string urlFirebaseOnline = "https://boomaway-2ccf0-default-rtdb.firebaseio.com/OnlineLevels/";
        private string urlFirebaseStory = "https://boomaway-2ccf0-default-rtdb.firebaseio.com/StoryLevels/";

        #region Save
        public bool saveWorld(string saveName,byte[] imageBytes)
        {
            urlFirebaseOnline = "https://boomaway-2ccf0-default-rtdb.firebaseio.com/OnlineLevels/"; 
            StartCoroutine(FindOnlineLevelWithSameNameToSave(saveName,imageBytes));
            return true;
        }

         IEnumerator FindOnlineLevelWithSameNameToSave(string name,byte[] imageBytes)
        {
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
                    int indexOfLevel = 0;
                    foreach (JSONNode player in data)
                    {
                        if (name == player["LevelName"] && player["user"].Equals(Grid.gameStateManager.usernameOnline))
                        {
                            int j = 0;
                            foreach (var key in data.Keys)
                            {
                                if(j == indexOfLevel)
                                {
                                    StartCoroutine(deleteWorldToFireBase(urlFirebaseOnline + key));
                                    break;
                                }
                                j++;
                            }
                            
                            
                        }
                        
                        indexOfLevel++;
                    }
                }
            }
            StartCoroutine(saveWorldToFireBase(name,imageBytes));
        }
        #endregion

        #region Load
        public void loadWorldFromFirebase(string name, SaveType savetype)
        {
            Grid.gameStateManager.levelLoaded= false;
            string url = "";
            switch (savetype)
            {
                case SaveType.Builder:
                    StartCoroutine(UnityRequestLevelOnline(name));
                    break;
                case SaveType.Story:
                    url = urlFirebaseStory + name + ".json";
                    StartCoroutine(UnityRequestLevelStory(url));
                    break;
            }

        }
        #endregion

        #region Helpers
        public void Clear()
        {
            makerTiles = GameObject.FindObjectsOfType<MakerTile>();
            foreach (var i in makerTiles)
            {
                Destroy(i.gameObject);
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
        #endregion


        #region Firebase
        #region Load
        IEnumerator UnityRequestLevelOnline(string name)
        {
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
                    foreach (JSONNode player in data)
                    {
                        if (name == player["LevelName"])
                        {
                            queryResultSAVE = (string)player["SAVE"];
                            queryResultSTATE = (string)player["STATE"];
                            break;
                        }
                    }
                }
            }
            byte[] bytesNewSAVE = System.Convert.FromBase64String(queryResultSAVE);

            try
            {
                var obj = Deserialize<Tile[]>(bytesNewSAVE);
                Clear();

                for (int i = 0; i < obj.Length; i++)
                {
                    Instantiate(makerTilePrefab[obj[i].id],
                    new Vector3(obj[i].x, obj[i].y, obj[i].z),
                    Quaternion.identity);
                }
            }
            catch
            {
                Debug.LogError($"Fallo Al Cargar Nivel");
            }
            //LEVEL STATE
            byte[] bytesNewSTATE = System.Convert.FromBase64String(queryResultSTATE);
            loadState(bytesNewSTATE);
            Grid.gameStateManager.levelLoaded= true;
        }
        IEnumerator UnityRequestLevelStory(string url)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();
                if (webRequest.isNetworkError)
                {
                    Debug.LogError("Error: " + webRequest.error);
                }
                else
                {
                    JSONNode data = JSON.Parse(webRequest.downloadHandler.text);
                    queryResultSAVE = (string)data["SAVE"];
                    queryResultSTATE = (string)data["STATE"];
                }
            }
            byte[] bytesNewSAVE = System.Convert.FromBase64String(queryResultSAVE);

            try
            {
                var obj = Deserialize<Tile[]>(bytesNewSAVE);
                Clear();

                for (int i = 0; i < obj.Length; i++)
                {
                    Instantiate(makerTilePrefab[obj[i].id],
                    new Vector3(obj[i].x, obj[i].y, obj[i].z),
                    Quaternion.identity);
                }
            }
            catch
            {
                Debug.LogError($"Fallo Al Cargar Nivel");
            }
            //LEVEL STATE
            byte[] bytesNewSTATE = System.Convert.FromBase64String(queryResultSTATE);
            loadState(bytesNewSTATE);
            Grid.gameStateManager.levelLoaded= true;
        }
        public bool loadState(byte[] bytesNewSTATE)
        {
            var state = Deserialize<State>(bytesNewSTATE);
            try
            {
                Grid.gameStateManager.ammo = state.ammo;
                Grid.gameStateManager.currentAmmo = state.ammo;
                return true;
            }
            catch
            {
                Debug.LogError($"Fallo Al Cargar Estado del Nivel");
                return false;
            }
        }
        #endregion
        #region Save
        IEnumerator saveWorldToFireBase(string levelName,byte[] imageBytes)
        {

            makerTiles = GameObject.FindObjectsOfType<MakerTile>();
            Tile[] t = new Tile[makerTiles.Length];

            for (int i = 0; i < makerTiles.Length; i++)
            {
                t[i] = new Tile(makerTiles[i].transform.position.x,
                makerTiles[i].transform.position.y,
                makerTiles[i].transform.position.z,
                makerTiles[i].id);
            }
            byte[] bytesSAVE = Serialize(t);
            string dataSAVE = System.Convert.ToBase64String(bytesSAVE);

            //State
            State state = new State();
            state.ammo = Grid.gameStateManager.ammo;

            byte[] bytesSTATE = Serialize(state);
            string dataSTATE = System.Convert.ToBase64String(bytesSTATE);
            
            string levelThumbnail = System.Convert.ToBase64String(imageBytes);

            string dq = ('"' + "");
            string bodyJsonString = "{" + dq + "LevelName" + dq + ":" + dq + (levelName) + dq + "," + dq + "SAVE" + dq + ":" + dq + (dataSAVE) + dq + "," + dq + "STATE" + dq + ":" + dq + (dataSTATE) + dq +"," + dq + "user" + dq + ":" + dq + (Grid.gameStateManager.usernameOnline) + dq + "," + dq + "Thumbnail" + dq + ":" + dq + (levelThumbnail) + dq +"}";
              

            var request = new UnityWebRequest(urlFirebaseOnline + ".json?auth="+Grid.gameStateManager.tokenFirebase, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
        }
        
        IEnumerator deleteWorldToFireBase(string urlFirebase)
        {
            var request = new UnityWebRequest(urlFirebase + ".json", "DELETE");
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
        }
        #endregion
        #endregion
    }
}