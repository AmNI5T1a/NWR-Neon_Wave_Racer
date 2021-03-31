using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace NWR
{

    public class LobbyManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private PlayerSettings _playerSettings;
        [SerializeField] private GameObject _player;

        [Space(10)]

        [SerializeField] private UI_Inventory _UI_Inventory;
        [Space(2)]
        [SerializeField] private GameObject _listOfRoads;
        [SerializeField] private GameObject _listOfGameStyles;
        [SerializeField] private GameObject _listOfCars;
        [SerializeField] private GameObject _returnArrow;

        [Header("Settings")]
        [SerializeField] public Inventory inventory;

        [Header("In play mode settings")]

        [HideInInspector] private bool roadMenuClosed = true;
        [HideInInspector] private bool gameStylesMenuClosed = true;
        [HideInInspector] private bool carsMenuClosed = true;


        [HideInInspector] private bool roadChoosen;
        [HideInInspector] private string choosenRoadName;


        [HideInInspector] private bool gameModeChoosen;
        [HideInInspector] private string choosenGameMode;
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
                StartCoroutine(LoadAsyncScene(1));
            }
        }

        IEnumerator LoadAsyncScene(byte sceneNumber)
        {
            Scene currentScene = SceneManager.GetActiveScene();

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneNumber, LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            SceneManager.MoveGameObjectToScene(_playerSettings.playerCar, SceneManager.GetSceneByBuildIndex(1));

            SceneManager.UnloadSceneAsync(currentScene);
        }
    }
}
