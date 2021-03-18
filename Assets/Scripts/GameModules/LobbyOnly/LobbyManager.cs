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
        [SerializeField] private GameObject _returnArrow;

        [Header("Settings")]
        [SerializeField] private Inventory inventory;

        [Header("In play mode settings")]
        [SerializeField] private bool roadMenuClosed = true;
        [SerializeField] private bool gameStylesMenuClosed = true;
        [SerializeField] private List<GameObject> _listOfOpenedWindows;
        void Awake()
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

            // Instanciate Inventory
            inventory = new Inventory();
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

            _listOfOpenedWindows[_listOfOpenedWindows.Count - 1].SetActive(false);
            _listOfOpenedWindows.Remove(_listOfOpenedWindows[_listOfOpenedWindows.Count - 1]);
        }

        public void TestDebugMessage()
        {
            Debug.Log("Button works well");
        }
    }
}
