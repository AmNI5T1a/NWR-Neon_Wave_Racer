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
        [SerializeReference] private UI_Manager _ui_manager;
        [SerializeReference] private LobbyManager _lobbyManager;

        [Header("Saved Settings: ")]
        [SerializeField] public uint money;

        [Space(33)]

        [SerializeField] public byte selectedCarID;
        [SerializeField] public byte selectedRoadID;
        [SerializeField] public byte selectedGameStyleID;

        [Space(33)]

        [SerializeField] public Car selectedCar;
        [SerializeField] public Road selectedRoad;
        [SerializeField] public GameStyle selectedGameStyle;

        [Space(33)]

        [SerializeField] public List<Byte> purchasedCarsIDs;
        [SerializeField] public List<Byte> purchasedRoadsID;
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
                    _ui_manager.UpdatePlayerSelectedCarInUIComponent(car);
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
                    _ui_manager.UpdatePlayerSelectedRoadInUIComponent(road);
                }
            }

            foreach (GameStyle style in _lobbyManager.inventory.GetListOfGameStyles())
            {
                if (style.GetPositionNumber() == loadedData.selectedGameModeID)
                {
                    selectedGameStyle = style;
                    selectedGameStyleID = loadedData.selectedGameModeID;
                    _ui_manager.UpdatePlayerSelectedGameStyleInUIComponent(style);
                }
            }

            _ui_manager.DestroyAllUIElements();
            _ui_manager.RefreshCarsMenu();
            _ui_manager.RefreshRoadsMenu();
            _ui_manager.RefreshGameStylesMenu();
        }
    }
}
