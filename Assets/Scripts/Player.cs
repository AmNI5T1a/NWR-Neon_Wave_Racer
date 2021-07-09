using System;
using System.Collections.Generic;
using UnityEngine;

namespace NWR.Modules
{
    public class Player : MonoBehaviour
    {
        public static Player Instance { get; private set; }

        public static event Action<List<int>> OnGetIDsOfBoughtCars;
        public static event Action<List<int>> OnGetIDsOfBoughtRoads;

        // ? Should i do this
        public static event SetPlayerCarInLobby OnSetPlayerCarInLobby;
        public delegate void SetPlayerCarInLobby(Car car);

        public static uint money;

        public static ushort selectedCarID;
        public static ushort selectedRoadID;
        public static ushort selectedGameModeID;

        public static List<int> boughtCars_List = new List<int>();
        public static List<int> boughtRoads_List = new List<int>();

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        void Start()
        {
            // * Update Player statistics
            LoadPlayerDataOnStart();

            // * Send as publisher event of bought items
            OnGetIDsOfBoughtCars?.Invoke(boughtCars_List);
            OnGetIDsOfBoughtRoads?.Invoke(boughtRoads_List);
        }

        private void LoadPlayerDataOnStart()
        {
            DataToSaveAndLoad loadedData = LoadSystem.Load();

            money = loadedData.money;

            selectedCarID = loadedData.selectedCarID;
            selectedRoadID = loadedData.selectedRoadID;
            selectedGameModeID = loadedData.selectedGameModeID;

            boughtCars_List = new List<int>(loadedData.ID_OfAllPurchasedCars);
            boughtRoads_List = new List<int>(loadedData.ID_OfAllPurchasedRoads);
        }
    }
}
