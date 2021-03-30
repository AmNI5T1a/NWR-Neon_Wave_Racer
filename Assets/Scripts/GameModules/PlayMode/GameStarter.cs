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
        [SerializeField] private GameObject _camera;

        [SerializeField] Transform carSpawnTransform;


        [Header("Settings")]
        [SerializeField] private float timeBeforeGiveControl;

        [Header("In game settings: ")]
        [SerializeField] GameObject playerCar;
        void Awake()
        {
            _inputManager.inputLocked = true;

            playerCar = PreGameSettings.playerSelectedCar;

            playerCar = Instantiate(_player.GetComponent<PlayerSettings>().playerCar, carSpawnTransform);

            _camera.GetComponent<CameraFollow>().SetTargetToFollow(playerCar);
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
