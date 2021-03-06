using UnityEngine;

namespace NWR
{
    [System.Serializable]
    public class Road : Item
    {
        [SerializeField] private uint price;
        [SerializeField] private bool boughtStatus;

        public uint GetPrice() => price;
        public bool BoughtStatus() => boughtStatus;
        public void PurchaseRoad()
        {
            this.boughtStatus = true;
        }
    }
}