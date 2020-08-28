using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

namespace BoomAway.Assets.Scripts.PreloadManager{

    [System.Serializable]
    public struct Tile
    {
        public float x,y,z;
        public int id;
        public Tile(float x, float y, float z, int id){
            this.x = x;
            this.y = y;
            this.z = z;
            this.id = id;
        }
    }

    public class SaveGameManger : MonoBehaviour
    {
        public MakerTile[] makerTilePrefab;
        MakerTile[] makerTiles;
        private string path;

        private void Awake() {
           path = Application.persistentDataPath + "/game_save";
        }
        public bool saveGame(string saveName)
        {   
            makerTiles = GameObject.FindObjectsOfType<MakerTile>();
            BinaryFormatter bf = new BinaryFormatter();

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Crear archivo de guardado
            FileStream file = new FileStream(path + "/"+ saveName + ".save", FileMode.Create, FileAccess.Write,FileShare.None);

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

            return true;
        }

        public bool loadGame(string loadName)
        {
            if (!File.Exists(path + "/" + loadName + ".save"))
            {
                
            Debug.Log("fail");
                return false;
            }
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(path + "/" + loadName + ".save", FileMode.Open, FileAccess.Read,FileShare.Read);

            try
            {
                var obj = (Tile[])bf.Deserialize(file);
                file.Close();

                Debug.Log(obj.Length);
                for (int i = 0; i < obj.Length; i++)
                {
                    
                    Instantiate(makerTilePrefab[obj[i].id],
                    new Vector3(obj[i].x, obj[i].y, obj[i].z),
                    Quaternion.identity);   
                }
                return true;
            }
            catch
            {
                Debug.LogError($"Fallo Al Cargar Archivo: {loadName}");
                file.Close();
                return false;
            }
        }

        public void Clear(){
            Debug.Log("hi");
            makerTiles = GameObject.FindObjectsOfType<MakerTile>();
            foreach (var i in makerTiles)
            {
                Destroy(i.gameObject);
            }
        }
    }

}
