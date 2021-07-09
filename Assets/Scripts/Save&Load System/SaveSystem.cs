using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


namespace NWR.Modules
{
    public static class SaveSystem
    {
        public static void Save()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/data.ddsjs";
            FileStream stream = new FileStream(path, FileMode.Create);

            DataToSaveAndLoad playerData = new DataToSaveAndLoad();

            formatter.Serialize(stream, playerData);
            stream.Close();
        }
    }
}
