using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR
{
    public class PlayerSettings : MonoBehaviour
    {
        [SerializeField] public uint playerMoney;
        [SerializeField] public GameObject playerCar;
        [SerializeField] public byte carPositionNumber;

        // TODO: add logic for road and gameMode
        [SerializeField] public byte selectedRoadNumber;
        [SerializeField] public byte selectedGameModeNumber;

        public void UpdatePlayerCar(Car car)
        {
            playerCar = car.GetCarAsGameObject();
            carPositionNumber = Convert.ToByte(car.GetPositionNumber());

            // ! Debug info
            // ! Delete after tests
            Debug.Log(carPositionNumber);

            playerCar.SetActive(true);
        }

        public void SavePlayerStats()
        {
            SaveSystem.Save(this);
        }

        public void LoadPlayerStats()
        {
            PlayerData loadedData = SaveSystem.Load();

            playerMoney = loadedData.money;
            carPositionNumber = loadedData.selectedCarNumber;
        }
    }
}
