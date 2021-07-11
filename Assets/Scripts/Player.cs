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


        public static EventHandler<PlayerSelectedItemIDsEventArgs> OnSendPlayerSelectedItemIDs;
        public class PlayerSelectedItemIDsEventArgs : EventArgs
        {
            public ushort car_ID;
            public ushort road_ID;
            public ushort gameMode_ID;
        }



        public uint money;

        public ushort selectedCarID;
        public ushort selectedRoadID;
        public ushort selectedGameModeID;

        public List<int> boughtCars_List = new List<int>();
        public List<int> boughtRoads_List = new List<int>();

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

            // * Loading all game objects and UI components
            OnGetIDsOfBoughtCars?.Invoke(boughtCars_List);
            OnGetIDsOfBoughtRoads?.Invoke(boughtRoads_List);

            OnSendPlayerSelectedItemIDs?.Invoke(this, new PlayerSelectedItemIDsEventArgs
            {
                car_ID = selectedCarID,
                road_ID = selectedRoadID,
                gameMode_ID = selectedGameModeID

            });
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
