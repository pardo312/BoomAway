using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;


namespace BoomAway.Assets.Scripts.PreloadManager
{
    public class WorldSaveManager : MonoBehaviour
    {
        public MakerTile[] makerTilePrefab;
        MakerTile[] makerTiles;
        [HideInInspector] public string rootPath;

        private void Awake()
        {
            rootPath = Application.persistentDataPath;
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
                //Crear archivo de guardado
                FileStream file = new FileStream(path + "/" + saveName + ".save", FileMode.Create, FileAccess.Write, FileShare.None);

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
                saveState(saveName,savetype);
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


        public void Clear()
        {
            makerTiles = GameObject.FindObjectsOfType<MakerTile>();
            foreach (var i in makerTiles)
            {
                Destroy(i.gameObject);
            }
        }
    }
}