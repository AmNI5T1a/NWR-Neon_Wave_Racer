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
        [SerializeField] private UI_Manager _UI_manager;

        [Space(2)]

        [Header("Inventory")]
        [SerializeField] public Inventory inventory;

        [Header("In play mode settings")]
        [HideInInspector] private bool gameIsLoading;

        [Space(2)]

        [SerializeField] public Car choosenCar;
        [SerializeField] public bool carIsChoosen;
        [SerializeField] private Road choosenRoad;
        [SerializeField] private bool roadIsChoosen;
        [SerializeField] private GameStyle choosenGameStyle;
        [SerializeField] private bool gameStyleIsChoosen;
        void Start()
        {
            // Lock input at the start of the game
            _inputManager.inputLocked = true;

            gameIsLoading = false;
        }
        public void UpdateSelectedRoad(Road item)
        {
            choosenRoad = item;

            roadIsChoosen = true;
        }

        public void UpdateSelectedGameMode(GameStyle gameStyle)
        {
            choosenGameStyle = gameStyle;

            gameStyleIsChoosen = true;
        }

        public void UpdateSelectedCar(Car car)
        {
            choosenCar = car;

            carIsChoosen = true;
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
            if (carIsChoosen && gameStyleIsChoosen && roadIsChoosen)
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

                SceneManager.MoveGameObjectToScene(_playerSettings.playerSelectedCar, SceneManager.GetSceneByBuildIndex(1));

                SceneManager.UnloadSceneAsync(currentScene);
            }
            else
            {
                Debug.LogWarning("Game is already loading...");
            }
        }
    }
}
