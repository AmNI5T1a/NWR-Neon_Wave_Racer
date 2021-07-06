using System;
using System.Collections.Generic;
using UnityEngine;

namespace NWR.Modules
{
    public class Player : MonoBehaviour
    {
        public static Player Instance { get; private set; }

        public static event Action<List<int>> onGetIDsBoughtCars;
        public static event Action<List<int>> onGetIDsBoughtRoads;

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

            // * Try to set which items was bought
            onGetIDsBoughtCars?.Invoke(boughtCars_List);
            onGetIDsBoughtRoads?.Invoke(boughtRoads_List);
        }

        private void LoadPlayerDataOnStart()
        {

            DataToSaveAndLoad loadedData = LoadSystem.Load();

            this.money = loadedData.money;

            this.money = loadedData.money;

            this.selectedCarID = loadedData.selectedCarID;
            this.selectedRoadID = loadedData.selectedRoadID;
            this.selectedGameModeID = loadedData.selectedGameModeID;

            this.boughtCars_List = new List<int>(loadedData.ID_OfAllPurchasedCars);
            this.boughtRoads_List = new List<int>(loadedData.ID_OfAllPurchasedRoads);
        }
    }
}
