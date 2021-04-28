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
        [SerializeField] public List<Byte> purchasedRoadsID;

        [SerializeField] public GameStyle selectedGameStyle;
        [SerializeField] public byte selectedGameStyleID;

        public void SavePlayerStats()
        {
            SaveSystem.Save(this);
        }

        public void LoadPlayerStats()
        {
            PlayerData loadedData = SaveSystem.Load();

            money = loadedData.money;
            _ui_manager.UpdateMoneyInUIComponent();

            foreach (Car car in _lobbyManager.inventory.GetListOfCars())
            {
                if (loadedData.selectedCarID == car.GetPositionNumber())
                {
                    _lobbyManager.UpdateSelectedCar(car);
                    _ui_manager.UpdateSelectedPlayerCarInUIComponent(car);
                }
            }
            purchasedCarsIDs = loadedData.purcahsedCarsID.OfType<byte>().ToList();
            foreach (Car car in _lobbyManager.inventory.GetListOfCars())
            {
                if (purchasedCarsIDs.Contains(car.GetPositionNumber()))
                    car.PurchaseCar();
            }


            purchasedRoadsID = loadedData.purchasedRoadsID.OfType<byte>().ToList();
            foreach (Road road in _lobbyManager.inventory.GetListOfRoads())
            {
                if (purchasedRoadsID.Contains(road.GetPositionNumber()))
                    road.PurchaseRoad();
            }

            foreach (Road road in _lobbyManager.inventory.GetListOfRoads())
            {
                if (loadedData.selectedRoadID == road.GetPositionNumber())
                {
                    _lobbyManager.UpdateSelectedRoad(road);
                    _ui_manager.UpdateSelectedPlayerRoadInUIComponent(road);
                }
            }

            _ui_manager.DestroyAllUIElements();
            _ui_manager.RefreshCarsMenu();
            _ui_manager.RefreshRoadsMenu();
            _ui_manager.RefreshGameStylesMenu();
        }
    }
}
