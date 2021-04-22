#pragma warning disable 414

using System.Collections;
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

        [SerializeField] private UI_Manager _UI_manager;
        [Space(2)]

        //TODO: this 4 strokes send to UI_Manager let him do it 
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
        [HideInInspector] private bool gameIsLoading;


        [HideInInspector] private bool roadChoosen;
        [HideInInspector] private string choosenRoadName;


        [HideInInspector] private bool gameModeChoosen;
        [HideInInspector] private string choosenGameMode;

        [HideInInspector] private bool carChoosen;
        [HideInInspector] private string choosenCarName;
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

            gameIsLoading = false;
        }
        public void SetRoad(Road item)
        {
            choosenRoadName = item.GetName();

            roadChoosen = true;
        }

        public void SetGameMode(GameStyle gameStyle)
        {
            choosenRoadName = gameStyle.GetName();

            gameModeChoosen = true;
        }

        public void SetCarName(Car car)
        {
            choosenCarName = car.GetName();

            carChoosen = true;
        }

        public bool BuyItemFromShop(in uint price)
        {
            if (_playerSettings.playerMoney >= price)
            {
                _playerSettings.playerMoney = _playerSettings.playerMoney - price;
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

        // ? Should i upgrade logic for a scene load system cause if playa tap double times without delay it will load two same scenes?
        IEnumerator LoadAsyncScene(byte sceneNumber)
        {
            if (!gameIsLoading)
            {
                gameIsLoading = true;

                Scene currentScene = SceneManager.GetActiveScene();

                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneNumber, LoadSceneMode.Additive);

                while (!asyncLoad.isDone)
                {
                    yield return null;
                }

                SceneManager.MoveGameObjectToScene(_playerSettings.playerCar, SceneManager.GetSceneByBuildIndex(1));

                SceneManager.UnloadSceneAsync(currentScene);
            }
            else
            {
                Debug.Log("Game is already loading...");
            }
        }
    }
}
