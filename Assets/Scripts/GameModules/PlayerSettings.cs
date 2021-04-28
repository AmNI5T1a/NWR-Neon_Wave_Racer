using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace NWR
{
    public class PlayerSettings : MonoBehaviour
    {
        [Header("References: ")]
        [SerializeField] private UI_Manager _ui_manager;
        [SerializeField] private LobbyManager _lobbyManager;

        [Header("Saved Settings: ")]
        [SerializeField] public uint money;

        [SerializeField] public Car selectedCar;
        [SerializeField] public byte selectedCarID;
        [SerializeField] public List<Byte> purchasedCarsIDs;

        [SerializeField] public Road selectedRoad;
        [SerializeField] public byte selectedRoadID;
        [SerializeField] public byte purchasedRoadsID;

        [SerializeField] public GameStyle selectedGameStyle;
        [SerializeField] public byte selectedGameStyleID;

        public void SavePlayerStats()
        {
            SaveSystem.Save(this);
        }

        // TODO: rework this method at all
        public void LoadPlayerStats()
        {
            PlayerData loadedData = SaveSystem.Load();

            money = loadedData.money;
            _ui_manager.UpdateMoneyInUIComponent();

            selectedCarID = loadedData.selectedCarID;

            foreach (Car car in _lobbyManager.inventory.GetListOfCars())
            {
                if (car.GetPositionNumber() == selectedCarID)
                {
                    _lobbyManager.UpdateSelectedCar(car);
                    _ui_manager.UpdateSelectedPlayerCarInUIComponent(car);
                }
            }

            purchasedCarsIDs = loadedData.purcahsedCarsID.OfType<byte>().ToList();
            foreach (Car car in _lobbyManager.inventory.GetListOfCars())
            {
                if (purchasedCarsIDs.Contains(car.GetPositionNumber()))
                {
                    car.PurchaseCar();
                }
            }

            _ui_manager.DestroyAllCarsUIElements();
            _ui_manager.RefreshCarsMenu();
        }

        public void LoadPlayerStatsTEST()
        {
            PlayerData loadedData = SaveSystem.Load();

            // TODO: update money score

            // TODO: find selected car with id 
            // TODO:    and
            // TODO: SetActive player's selected car, show in UI selected car name
            // TODO: set in LobbyManager script: car as gameobject, car as Car, and bool as true in carIsChoosen

            // TODO: get list of purchased roads and update UI
            // TODO: get selectedRoadID from loadedData and update selected road and in UI component

            // TODO: get selected GameModeID and update gameMode as GameObject and UI component
        }
    }
}
