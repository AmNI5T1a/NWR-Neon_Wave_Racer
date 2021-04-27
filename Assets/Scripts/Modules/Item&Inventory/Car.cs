using UnityEngine;

namespace NWR
{
    [System.Serializable]
    public class Car : Item
    {
        [SerializeField] private uint price;
        [SerializeField] private bool boughtStatus;
        [SerializeField] private GameObject carPrefab;

        public uint GetPrice() => price;

        public bool BoughtStatus() => boughtStatus;
        public void PurchaseCar()
        {
            this.boughtStatus = true;
        }

        public GameObject GetCarAsGameObject() => carPrefab;
    }
}