using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

namespace BoomAway.Assets.Scripts.PreloadManager{


    public class SaveGameManger : MonoBehaviour
    {
        [HideInInspector]
        public string path;

        private void Awake() {
           path = Application.persistentDataPath + "/game_save";
        }
        public bool saveGame(string saveName)
        {   
            BinaryFormatter bf = new BinaryFormatter();

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Crear archivo de guardado
            FileStream file = new FileStream(path + "/"+ saveName + ".save", FileMode.Create, FileAccess.Write,FileShare.None);

            //TODO

            //bf.Serialize(file, );
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

                //TODO

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
    }
}
