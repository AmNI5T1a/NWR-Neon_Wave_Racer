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

        [Header("Settings")]
        [SerializeField] public Inventory inventory;

        [Header("In play mode settings")]
        [HideInInspector] private bool gameIsLoading;

        [Space(1)]

        [HideInInspector] private bool roadChoosen;
        [HideInInspector] private string choosenRoadName;

        [HideInInspector] private bool gameModeChoosen;

        [HideInInspector] private bool carChoosen;
        [HideInInspector] private string choosenCarName;
        void Start()
        {
            // Lock input at the start of the game
            _inputManager.inputLocked = true;

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
                Debug.LogWarning("Game is already loading...");
            }
        }
    }
}
