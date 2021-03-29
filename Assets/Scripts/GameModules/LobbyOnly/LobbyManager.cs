using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NWR
{

    public class LobbyManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InputManager _inputManager;

        [SerializeField] private GameObject _listOfRoads;
        [SerializeField] private GameObject _listOfGameStyles;
        [SerializeField] private GameObject _listOfCars;
        [SerializeField] private GameObject _returnArrow;

        [SerializeField] private UI_Inventory _UI_Inventory;

        [SerializeField] private PlayerSettings _playerSettings;

        [Header("Settings")]
        [SerializeField] public Inventory inventory;
        [SerializeField] public int money;

        [Header("In play mode settings")]
        [SerializeField] private List<GameObject> _listOfOpenedWindows;

        [Space(5)]

        [SerializeField] private bool roadMenuClosed = true;
        [SerializeField] private bool gameStylesMenuClosed = true;
        [SerializeField] private bool carsMenuClosed = true;

        [Space(5)]

        [SerializeField] private bool roadChoosen;
        [SerializeField] private string choosenRoadName;

        [Space(5)]

        [SerializeField] private bool gameModeChoosen;
        [SerializeField] private string choosenGameMode;
        void Start()
        {
            // Lock input at the start of the game
            _inputManager.inputLocked = true;

            // Close road menu at the start of the game
            roadMenuClosed = true;
            _listOfRoads.SetActive(false);

            // Close game styles at the start of the game
            gameStylesMenuClosed = true;
            _listOfGameStyles.SetActive(false);

            // Close return arrow at the start of the game
            _returnArrow.SetActive(false);

            // Close car list
            carsMenuClosed = true;
            _listOfCars.SetActive(false);

            // Instanciate Inventory
            inventory = new Inventory();
            RefreshInventory();
        }


        void Update()
        {
            if (_listOfOpenedWindows.Count >= 1)
            {
                _returnArrow.SetActive(true);
            }
            else if (_listOfOpenedWindows.Count == 0)
            {
                _returnArrow.SetActive(false);
            }
        }

        void RefreshInventory()
        {
            inventory.AddItem(new Item { itemType = Item.ItemType.Road, amount = 1, boughtStatus = true, price = 228, posNumber = 1, name = "Abell 520" });
            inventory.AddItem(new Item { itemType = Item.ItemType.Road, amount = 1, boughtStatus = false, price = 10000, posNumber = 2, name = "Sombrero" });
            inventory.AddItem(new Item { itemType = Item.ItemType.GameStyle, amount = 1, boughtStatus = true, price = 100, posNumber = 1, name = "One direction" });
            inventory.AddItem(new Item { itemType = Item.ItemType.GameStyle, amount = 1, boughtStatus = true, price = 200, posNumber = 2, name = "Oncoming traffic" });
            inventory.AddItem(new Item { itemType = Item.ItemType.Car, amount = 1, boughtStatus = true, price = 1, posNumber = 1, name = "Golf GTI" });
            inventory.AddItem(new Item { itemType = Item.ItemType.Car, amount = 1, boughtStatus = false, price = 32000, posNumber = 2, name = "Subaru WRX" });
            inventory.AddItem(new Item { itemType = Item.ItemType.Car, amount = 1, boughtStatus = false, price = 75000, posNumber = 3, name = "Dodge Charger" });


            _UI_Inventory.RefreshInventory();
        }
        #region InteractionWithUIElements
        public void OpenOrCloseCarsMenu()
        {
            if (carsMenuClosed)
            {
                carsMenuClosed = false;
                _listOfOpenedWindows.Add(_listOfCars);
                _listOfCars.SetActive(true);
            }
            else if (!carsMenuClosed)
            {
                carsMenuClosed = true;
                _listOfOpenedWindows.Remove(_listOfCars);
                _listOfCars.SetActive(false);
            }
        }

        public void OpenOrCloseSelectRoadMenu()
        {
            if (roadMenuClosed)
            {
                _listOfRoads.SetActive(true);
                _listOfOpenedWindows.Add(_listOfRoads);
                roadMenuClosed = false;
            }
            else if (!roadMenuClosed)
            {
                _listOfRoads.SetActive(false);
                _listOfOpenedWindows.Remove(_listOfRoads);
                roadMenuClosed = true;
            }
        }

        public void OpenOrCloseSelectGameStyle()
        {
            if (gameStylesMenuClosed)
            {
                _listOfGameStyles.SetActive(true);
                _listOfOpenedWindows.Add(_listOfGameStyles);
                gameStylesMenuClosed = false;
            }
            else if (!gameStylesMenuClosed)
            {
                _listOfGameStyles.SetActive(false);
                _listOfOpenedWindows.Remove(_listOfGameStyles);
                gameStylesMenuClosed = true;
            }
        }

        public void CloseLastOpenedWindowWithReturnArrow()
        {
            if (_listOfOpenedWindows[_listOfOpenedWindows.Count - 1] == _listOfRoads)
            {
                roadMenuClosed = true;
            }
            else if (_listOfOpenedWindows[_listOfOpenedWindows.Count - 1] == _listOfGameStyles)
            {
                gameStylesMenuClosed = true;
            }
            else if (_listOfOpenedWindows[_listOfOpenedWindows.Count - 1] == _listOfCars)
            {
                carsMenuClosed = true;
            }

            _listOfOpenedWindows[_listOfOpenedWindows.Count - 1].SetActive(false);
            _listOfOpenedWindows.Remove(_listOfOpenedWindows[_listOfOpenedWindows.Count - 1]);
        }
        #endregion
        public void SetRoad(Item item)
        {
            choosenRoadName = item.name;

            roadChoosen = true;
        }

        public void SetGameMode(Item item)
        {
            choosenRoadName = item.name;

            gameModeChoosen = true;
        }

        public bool BuyItemFromShop(ref Item itemToBuy)
        {
            if (_playerSettings.playerMoney >= itemToBuy.price)
            {
                _playerSettings.playerMoney = _playerSettings.playerMoney - itemToBuy.price;
                itemToBuy.boughtStatus = true;

                return true;
            }
            else
            {
                return false;
            }
        }

        public void StartAGame()
        {
            if (gameModeChoosen && roadChoosen)
            {

            }
        }
    }
}
