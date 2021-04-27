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

        [Header("Stats: ")]
        [SerializeField] public uint playerMoney;
        [SerializeField] public GameObject playerSelectedCar;
        [SerializeField] public byte playerSelectedCarID;

        [SerializeField] public List<Byte> purchasedCarsIDs;

        public void UpdatePlayerCar(Car car)
        {
            playerSelectedCar = car.GetCarAsGameObject();
            playerSelectedCarID = car.GetPositionNumber();
            _lobbyManager.choosenCar = car;

            playerSelectedCar.SetActive(true);
        }

        public void SavePlayerStats()
        {
            SaveSystem.Save(this);
        }

        // TODO: rework this method at all
        public void LoadPlayerStats()
        {
            PlayerData loadedData = SaveSystem.Load();

            playerMoney = loadedData.money;
            _ui_manager.UpdateMoneyScore();

            playerSelectedCarID = loadedData.selectedCarID;

            foreach (Car car in _lobbyManager.inventory.GetListOfCars())
            {
                if (car.GetPositionNumber() == playerSelectedCarID)
                {
                    _lobbyManager.choosenCar = car;
                    _lobbyManager.carIsChoosen = true;

                    playerSelectedCar = car.GetCarAsGameObject();
                    playerSelectedCar.SetActive(true);
                    car.ChangeSelectedStatus();

                    _ui_manager.UpdateSelectedPlayerCarInUIComponent(car);
                }
            }

            purchasedCarsIDs = loadedData.purcahsedCarsIDs.OfType<byte>().ToList();
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
    }
}
