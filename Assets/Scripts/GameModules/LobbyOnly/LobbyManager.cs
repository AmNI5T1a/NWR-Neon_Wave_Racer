#pragma warning disable 414

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;


namespace NWR
{

    public class LobbyManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private PlayerSettings _playerSettings;
        [SerializeField] private UI_Manager _UI_manager;

        [Space(2)]

        [Header("Main objects")]
        [SerializeField] public Inventory inventory;
        [SerializeField] public GameObject playerCarObject;

        [Header("In play mode settings")]
        [HideInInspector] private bool gameIsLoading;

        void Start()
        {
            // Lock input at the start of the game
            _inputManager.inputLocked = true;

            gameIsLoading = false;
        }

        public void UpdateSelectedCar(Car newCar)
        {
            if (playerCarObject != null)
                playerCarObject.SetActive(false);

            //* This is very important it's stores and shows in Lobby player's car and load a game with this car
            playerCarObject = newCar.GetCarAsGameObject();
            //* __________________________________________________________________

            _playerSettings.selectedCar = newCar;
            _playerSettings.selectedCarID = newCar.GetPositionNumber();

            playerCarObject.SetActive(true);
        }
        public void UpdateSelectedRoad(Road newRoad)
        {
            _playerSettings.selectedRoad = newRoad;
            _playerSettings.selectedRoadID = newRoad.GetPositionNumber();
        }
        public void UpdateSelectedGameMode(GameStyle newGameStyle)
        {
            _playerSettings.selectedGameStyle = newGameStyle;
            _playerSettings.selectedGameStyleID = newGameStyle.GetPositionNumber();
        }

        public bool BuyItemFromShop(in uint price)
        {
            if (_playerSettings.money >= price)
            {
                _playerSettings.money = _playerSettings.money - price;
                return true;
            }
            return false;
        }

        public void StartAGame()
        {
            if (_playerSettings.selectedCar != null && _playerSettings.selectedGameStyle != null && _playerSettings.selectedGameStyle != null)
            {
                StartCoroutine(LoadAsyncScene(1));
            }
            else
            {
                // TODO: Can create a pop-up window and shop error there
                Debug.LogError("CAN'T START A GAME CAR/ROAD/GAMESTYLE IS/ARE EMPTY");
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

                SceneManager.MoveGameObjectToScene(playerCarObject, SceneManager.GetSceneByBuildIndex(1));

                SceneManager.UnloadSceneAsync(currentScene);
            }
            else
            {
                Debug.LogWarning("Game is already loading...");
            }
        }
    }
}
