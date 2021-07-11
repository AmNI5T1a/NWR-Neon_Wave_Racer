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



        public static event EventHandler<OnSendPlayerSelectedItemsEventArgs> OnSendPlayerSelectedItems;
        public class OnSendPlayerSelectedItemsEventArgs : EventArgs
        {
            public ItemAndStats<Car> playerCar;
            public ItemAndStats<Road> playerRoad;
            public ItemAndStats<GameMode> playerGameMode;
        }



        public static event EventHandler<OnSendItemsEventArgs> OnSendItems;
        public class OnSendItemsEventArgs : EventArgs
        {
            public List<ItemAndStats<Car>> cars_List;
            public List<ItemAndStats<Road>> roads_List;
            public List<ItemAndStats<GameMode>> gameModes_List;
        }

        void Awake()
        {
            Player.OnGetIDsOfBoughtCars += SetPurchasedStatusForCars;
            Player.OnGetIDsOfBoughtRoads += SetPurchasedStatusForRoads;

            Player.OnSendPlayerSelectedItemIDs += FindAndSendItemsInformation;
        }

        void Start()
        {
            OnSendItems?.Invoke(this, new OnSendItemsEventArgs
            {
                cars_List = this.cars_list,
                roads_List = this.roads_list,
                gameModes_List = this.gameModes_list
            });
        }


        // ? Can i recode it to 1 method
        // ! 2 same methods cause id of each child from item starts with 0
        // ! Lists don't know about each other
        #region LoadAlreadyPurchasedCars
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
        #endregion LoadAlreadyPurchasedCars
        private void FindAndSendItemsInformation(object sender, Player.PlayerSelectedItemIDsEventArgs e)
        {
            OnSendPlayerSelectedItemsEventArgs playerItems = new OnSendPlayerSelectedItemsEventArgs();

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

            OnSendPlayerSelectedItems?.Invoke(this, playerItems);
        }
    }
}

