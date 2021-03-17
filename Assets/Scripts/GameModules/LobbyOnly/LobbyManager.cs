using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NWR
{

    public class LobbyManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InputManager _inputManager;

        [Header("In play mode settings")]
        [SerializeField] private bool selectRoadMenuClosed = true;
        void Awake()
        {
            _inputManager.inputLocked = true;
            selectRoadMenuClosed = true;
        }


        public void CheckConditionsAndStartAGame()
        {

        }

        public void OpenOrCloseSelectRoadMenu()
        {
            if (selectRoadMenuClosed)
                OpenSelectRoadMenu();
            else if (!selectRoadMenuClosed)
                CloseSelectRoadMenu();
        }

        public void OpenSelectRoadMenu()
        {

        }

        public void CloseSelectRoadMenu()
        {

        }

        public void TestDebugMessage()
        {
            Debug.Log("Button works well");
        }
    }
}
