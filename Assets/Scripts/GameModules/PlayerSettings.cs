using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public void UpdatePlayerCar(Car car)
        {
            playerSelectedCar = car.GetCarAsGameObject();
            playerSelectedCarID = car.GetPositionNumber();

            playerSelectedCar.SetActive(true);
        }

        public void SavePlayerStats()
        {
            SaveSystem.Save(this);
        }

        public void LoadPlayerStats()
        {
            PlayerData loadedData = SaveSystem.Load();

            playerMoney = loadedData.money;
            _ui_manager.UpdateMoneyScore();

            playerSelectedCarID = loadedData.selectedCarID;

            foreach (Car car in _lobbyManager.inventory.GetListOfCars())
            {
                // ! TEST SUBJECT
                Debug.Log(car.GetName());
                Debug.Log(playerSelectedCarID + " " + car.GetPositionNumber());

                if (car.GetPositionNumber() == playerSelectedCarID)
                {
                    playerSelectedCar = car.GetCarAsGameObject();
                    playerSelectedCar.SetActive(true);
                    car.ChangeSelectedStatus();
                }
            }
        }
    }
}
