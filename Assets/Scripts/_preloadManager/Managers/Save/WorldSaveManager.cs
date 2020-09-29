using System.IO;
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
        private string queryResultSAVE ="";
        private string queryResultSTATE ="";
        

        private string urlFirebaseOnline = "https://boomaway-10de3.firebaseio.com/OnlineLevels/";
        private string urlFirebaseStory = "https://boomaway-10de3.firebaseio.com/StoryLevels/";
        
        private List<string> onlineLevelsNames;

        private void Awake()
        {
            rootPath = "./data";
        }
        #region Save
        #region World
        public bool saveWorld(string saveName, SaveType savetype)
        {
            string path = rootPath;
            makerTiles = GameObject.FindObjectsOfType<MakerTile>();
            BinaryFormatter bf = new BinaryFormatter();

            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }
            switch (savetype)
            {
                case SaveType.Builder:
                    path += "/saved_worlds/";
                    break;
                case SaveType.Story:
                    path += "/story_worlds/";
                    break;
            }
            string pathToSave = path + saveName + ".save";
            //Crear archivo de guardado
            FileStream file = new FileStream(pathToSave, FileMode.Create, FileAccess.Write, FileShare.None);

            Tile[] t = new Tile[makerTiles.Length];

            for (int i = 0; i < makerTiles.Length; i++)
            {
                t[i] = new Tile(makerTiles[i].transform.position.x,
                makerTiles[i].transform.position.y,
                makerTiles[i].transform.position.z,
                makerTiles[i].id);
            }
            bf.Serialize(file, t);
            file.Close();
            saveState(saveName, savetype);
            StartCoroutine(saveWorldToFireBase(path, saveName));
            return true;
        }
        #endregion
        #region State
        public bool saveState(string saveName, SaveType savetype)
        {
            string path = rootPath;
            makerTiles = GameObject.FindObjectsOfType<MakerTile>();
            BinaryFormatter bf = new BinaryFormatter();

            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }
            switch (savetype)
            {
                case SaveType.Builder:
                    path += "/saved_worlds/";
                    break;
                case SaveType.Story:
                    path += "/story_worlds/";
                    break;
            }
            //Crear archivo de guardado
            FileStream file = new FileStream(path + "/" + saveName + ".state", FileMode.Create, FileAccess.Write, FileShare.None);

            State state = new State();

            state.ammo = Grid.gameStateManager.ammo;

            bf.Serialize(file, state);
            file.Close();

            return true;
        }
        #endregion
        #endregion

        #region Load
        #region World
        public bool loadWorld(string loadName)
        {
            if (!File.Exists(loadName))
            {
                Debug.Log("fail, path:" + loadName);
                return false;
            }
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(loadName, FileMode.Open, FileAccess.Read, FileShare.Read);

            try
            {
                var obj = (Tile[])bf.Deserialize(file);
                file.Close();
                Clear();

                for (int i = 0; i < obj.Length; i++)
                {
                    Instantiate(makerTilePrefab[obj[i].id],
                    new Vector3(obj[i].x, obj[i].y, obj[i].z),
                    Quaternion.identity);
                }
                string statePath = loadName.Replace(".save", ".state");
                loadState(statePath);
                return true;
            }
            catch
            {
                Debug.LogError($"Fallo Al Cargar Archivo: {loadName}");
                file.Close();
                return false;
            }
        }

        public bool loadWorldFromFolder(string loadName, SaveType savetype)
        {
            var path = rootPath;
            switch (savetype)
            {
                case SaveType.Builder:
                    path += "/saved_worlds/";
                    break;
                case SaveType.Story:
                    path += "/story_worlds/";
                    break;
            }
            path += loadName;
            path += ".save";
            if (!File.Exists(path))
            {
                Debug.LogError("fail, path:" + path);
                return false;
            }
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

            try
            {
                var obj = (Tile[])bf.Deserialize(file);
                file.Close();
                Clear();

                for (int i = 0; i < obj.Length; i++)
                {
                    Instantiate(makerTilePrefab[obj[i].id],
                    new Vector3(obj[i].x, obj[i].y, obj[i].z),
                    Quaternion.identity);
                }
                string statePath = path.Replace(".save", ".state");
                loadState(statePath);
                return true;
            }
            catch
            {
                Debug.LogError($"Fallo Al Cargar Archivo: {path}");
                file.Close();
                return false;
            }
        }
        public void loadWorldFromFirebase(string name,SaveType savetype)
        {
            string url = "";
            switch (savetype)
            {
                case SaveType.Builder:
                    StartCoroutine(UnityRequestLevelOnline(name));
                    break;
                case SaveType.Story:
                    url = urlFirebaseStory + name +".json";
                    StartCoroutine(UnityRequestLevelStory(url));
                    break;
            }
            
        }
        #endregion
        #region State
        public bool loadState(string loadName)
        {
            if (!File.Exists(loadName))
            {
                Debug.Log("fail, path:" + loadName);
                return false;
            }
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(loadName, FileMode.Open, FileAccess.Read, FileShare.Read);

            try
            {
                var state = (State)bf.Deserialize(file);
                Grid.gameStateManager.ammo = state.ammo;
                Grid.gameStateManager.currentAmmo = state.ammo;
                file.Close();
                return true;
            }
            catch
            {
                Debug.LogError($"Fallo Al Cargar Archivo: {loadName}");
                file.Close();
                return false;
            }
        }
        #endregion
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
        #endregion

        
        #region Firebase
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
                        foreach(JSONNode player in data)
                        {
                            if(name == player["LevelName"])
                            {
                                Debug.Log("Nombre:"+name);
                                queryResultSAVE = (string)player["SAVE"];
                                queryResultSTATE = (string)player["STATE"];
                            }
                            else{
                                Debug.Log("No name:"+name);
                            }
                                
                        }
                    }
                }
                byte[] bytesNewSAVE = System.Convert.FromBase64String(queryResultSAVE);
                File.WriteAllBytes(rootPath + "/story_worlds/temp_level.save", bytesNewSAVE);

                byte[] bytesNewSTATE = System.Convert.FromBase64String(queryResultSTATE);
                File.WriteAllBytes(rootPath + "/story_worlds/temp_level.state", bytesNewSTATE);

                loadWorldFromFolder("temp_level",SaveType.Story);
            }

            
            IEnumerator UnityWebFindOnlienLevelWithName(string name)
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
                        foreach(JSONNode player in data)
                        {
                            if(name == player["LevelName"])
                                onlineLevelsNames.Add(player["LevelName"]);
                        }
                    }
                }
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
                File.WriteAllBytes(rootPath + "/story_worlds/temp_level.save", bytesNewSAVE);

                byte[] bytesNewSTATE = System.Convert.FromBase64String(queryResultSTATE);
                File.WriteAllBytes(rootPath + "/story_worlds/temp_level.state", bytesNewSTATE);

                loadWorldFromFolder("temp_level",SaveType.Story);
            }
            IEnumerator saveWorldToFireBase(string path, string levelName)
            {
                byte[] bytesSAVE = File.ReadAllBytes(path + levelName+".save");
                string dataSAVE = System.Convert.ToBase64String(bytesSAVE);
                
                byte[] bytesSTATE = File.ReadAllBytes(path + levelName+".state");
                string dataSTATE = System.Convert.ToBase64String(bytesSTATE);

                string dq  = ('"' + "" );
                string bodyJsonString ="{"+dq+"LevelName"+dq+":"+dq+ (levelName) +dq+"," +dq+"SAVE"+dq+":"+dq+ (dataSAVE) +dq+"," + dq +"STATE"+dq+":"+dq+ (dataSTATE) +dq+"}";
                
                Debug.Log(bodyJsonString);
                var request = new UnityWebRequest(urlFirebaseOnline+".json", "POST");
                byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
                request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
                yield return request.SendWebRequest();
            }
        #endregion
    }
}