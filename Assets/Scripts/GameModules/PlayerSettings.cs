using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR
{
    public class PlayerSettings : MonoBehaviour
    {
        [SerializeField] public GameObject playerCar;
        [SerializeField] public uint playerMoney;

        public void UpdatePlayerCar(Car car)
        {
            playerCar = car.GetCarAsGameObject();
            playerCar.SetActive(true);
        }
    }
}
