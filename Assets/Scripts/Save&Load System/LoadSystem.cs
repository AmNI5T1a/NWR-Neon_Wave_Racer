using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NWR.Modules
{
    public static class LoadSystem
    {
        public static DataToSaveAndLoad Load()
        {
            string path = Application.persistentDataPath + "/data.ddsjs";

            if (File.Exists(path))
            {
                DataToSaveAndLoad loadedData = GetDataFromSave(path);
                return loadedData;
            }
            else
            {
                DataToSaveAndLoad createdData = CreateSaveBecauseFirstEntryToGame(path);
                return createdData;
            }
        }

        private static DataToSaveAndLoad GetDataFromSave(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DataToSaveAndLoad data = formatter.Deserialize(stream) as DataToSaveAndLoad;

            stream.Close();
            return data;
        }

        private static DataToSaveAndLoad CreateSaveBecauseFirstEntryToGame(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            DataToSaveAndLoad newData = new DataToSaveAndLoad()
            {
                money = 9900,
                selectedCarID = 0,
                selectedRoadID = 2,
                selectedGameModeID = 1,
                ID_OfAllPurchasedCars = new int[1] { 0 },
                ID_OfAllPurchasedRoads = new int[1] { 2 }
            };

            formatter.Serialize(stream, newData);
            stream.Close();
            return newData;
        }
    }
}