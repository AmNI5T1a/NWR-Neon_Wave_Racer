using System;
using System.Collections.Generic;
using UnityEngine;
using NWR.Lobby;

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

        [SerializeField] public List<ItemAndStats<Car>> cars_list;
        [SerializeField] public List<ItemAndStats<Road>> roads_list;
        [SerializeField] public List<ItemAndStats<GameMode>> gameModes_list;
        public static event EventHandler<OnFindPlayerSelectedItemsEventArgs> OnFindPlayerSelectedItems;
        public class OnFindPlayerSelectedItemsEventArgs : EventArgs
        {
            public ItemAndStats<Car> playerCar;
            public ItemAndStats<Road> playerRoad;
            public ItemAndStats<GameMode> playerGameMode;
        }


        // ! Create here actions for every item 


        void Awake()
        {
            Player.OnGetIDsOfBoughtCars += SetPurchasedStatusForCars;
            Player.OnGetIDsOfBoughtRoads += SetPurchasedStatusForRoads;

            Player.OnSendPlayerSelectedItemIDs += FindAndSendItemsInformation;
        }


        // ? Can i recode it to 1 method
        // ! 2 same methods cause id of each child from item starts with 0
        // ! Lists don't know about each other
        private void SetPurchasedStatusForCars(List<int> IDs_ofPurchasedItems)
        {
            foreach (ItemAndStats<Car> item in cars_list)
            {
                item.isBought = IDs_ofPurchasedItems.Contains(item.item.GetID());
            }
        }

        private void SetPurchasedStatusForRoads(List<int> IDs_ofPurchasedItems)
        {
            foreach (ItemAndStats<Road> item in roads_list)
            {
                item.isBought = IDs_ofPurchasedItems.Contains(item.item.GetID());
            }
        }

        private void FindAndSendItemsInformation(object sender, Player.PlayerSelectedItemIDsEventArgs e)
        {
            OnFindPlayerSelectedItemsEventArgs playerItems = new OnFindPlayerSelectedItemsEventArgs();

            foreach (ItemAndStats<Car> car in cars_list)
            {
                if (car.item.GetID() == e.car_ID)
                {
                    playerItems.playerCar = car;
                }
            }


            foreach (ItemAndStats<Road> road in roads_list)
            {
                if (road.item.GetID() == e.road_ID)
                {
                    playerItems.playerRoad = road;
                }
            }

            foreach (ItemAndStats<GameMode> gm in gameModes_list)
            {
                if (gm.item.GetID() == e.gameMode_ID)
                {
                    playerItems.playerGameMode = gm;
                }
            }

            OnFindPlayerSelectedItems?.Invoke(this, playerItems);
        }
    }
}

