using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NWR
{
    public static class SaveSystem
    {
        public static void Save(PlayerSettings player)
        {
            Debug.LogWarning("Saving game...");

            BinaryFormatter formatter = new BinaryFormatter();

            string path = Application.persistentDataPath + "/PlayerData.dddsj";
            FileStream stream = new FileStream(path, FileMode.Create);

            PlayerData data = new PlayerData(player: player);

            formatter.Serialize(stream, data);

            stream.Close();
        }

        public static PlayerData Load()
        {
            Debug.LogWarning("Loading game...");
            string path = Application.persistentDataPath + "/PlayerData.dddsj";

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                PlayerData loadedData = formatter.Deserialize(stream) as PlayerData;

                stream.Close();
                return loadedData;
            }
            else
            {
                Debug.LogError("Save file doens't found");
                return null;
            }
        }
    }
}
