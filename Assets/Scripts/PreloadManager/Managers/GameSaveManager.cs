using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

namespace BoomAway.Assets.Scripts.PreloadManager{
    public class GameSaveManager : MonoBehaviour
    {
        private string path;

        private void Awake() {
           path = Application.persistentDataPath + "/game_save";
        }
        public bool saveGame(string saveName, object saveData)
        {
            BinaryFormatter bf = getBinaryFormatter();

            if (Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Crear archivo de guardado
            FileStream file = File.Create(path + saveName + ".save");


            bf.Serialize(file, saveData);

            file.Close();

            return true;
        }

        public object loadGame(string savePath)
        {

            if (!File.Exists(savePath))
            {
                return null;
            }

            BinaryFormatter bf = getBinaryFormatter();

            //Crear archivo de guardado
            FileStream file = File.Open(savePath, FileMode.Open);

            try
            {
                object save = bf.Deserialize(file);
                file.Close();
                return save;
            }
            catch
            {
                Debug.LogError($"Fallo Al Cargar Archivo: {savePath}");
                file.Close();
                return null;
            }
        }

        public BinaryFormatter getBinaryFormatter()
        {
            BinaryFormatter bf = new BinaryFormatter();

            return bf;
        }
    }

}
