using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NWR
{

    public class LobbyManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InputManager _inputManager;


        void Awake()
        {
            _inputManager.inputLocked = true;
        }
    }
}
