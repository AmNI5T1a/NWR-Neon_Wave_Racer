using System;
using System.Collections.Generic;
using UnityEngine;

namespace NWR.Modules
{
    public class Assets : MonoBehaviour
    {
        [System.Serializable]
        public class ItemAndStats<T> where T : Item
        {
            [SerializeField] public T item;
            [SerializeField] public bool isBought;
        }

        [SerializeField] private List<ItemAndStats<Car>> cars_list;
        [SerializeField] private List<ItemAndStats<Road>> roads_list;

        public static event SendCar OnSendCar;
        public delegate void SendCar(ItemAndStats<Car> car);

        void Awake()
        {
            Player.OnGetIDsOfBoughtCars += SetPurchasedStatusForCars;
            Player.OnGetIDsOfBoughtRoads += SetPurchasedStatusForRoads;
        }


        // ? Can i recode it to 1 method
        // ! 2 same methods cause id of each child from item starts with 0
        // ! Lists don't know about each other
        private void SetPurchasedStatusForCars(List<int> IDs_ofPurchasedItems)
        {
            foreach (ItemAndStats<Car> item in cars_list)
            {
                item.isBought = IDs_ofPurchasedItems.Contains(item.item.GetID());
                OnSendCar?.Invoke(item);
            }
        }

        private void SetPurchasedStatusForRoads(List<int> IDs_ofPurchasedItems)
        {
            foreach (ItemAndStats<Road> item in roads_list)
            {
                item.isBought = IDs_ofPurchasedItems.Contains(item.item.GetID());
            }
        }



    }
}
