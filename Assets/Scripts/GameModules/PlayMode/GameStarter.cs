using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR
{
    public class GameStarter : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private GameObject _player;


        [Header("Settings")]
        [SerializeField] private float timeBeforeGiveControl;
        void Awake()
        {
            _inputManager.inputLocked = true;

        }

        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");

            ShowErrorUfPlayerNotFound();
        }

        void Update()
        {

            if (_inputManager.inputLocked)
            {
                _player.GetComponent<CarController>().AddForceWhileInputLocked();
            }

            if (timeBeforeGiveControl <= 0)
            {
                _inputManager.inputLocked = false;
            }
            else
            {
                timeBeforeGiveControl -= Time.deltaTime;
            }
        }

        void ShowErrorUfPlayerNotFound()
        {
            if (_player == null)
            {
                Debug.LogError("Player doesn't found");
            }
        }
    }
}
