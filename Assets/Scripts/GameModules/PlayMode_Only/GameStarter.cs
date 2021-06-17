using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR
{
    public class GameStarter : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private GameObject _camera;

        [SerializeField] private Transform carSpawnTransform;


        [Header("Settings")]
        [SerializeField] private float timeBeforeGiveControl;

        [Header("In game settings: ")]
        [SerializeField] GameObject playerCar;
        void Awake()
        {
            _inputManager.inputLocked = true;

            playerCar = GameObject.FindGameObjectWithTag("Car");

            playerCar.transform.position = carSpawnTransform.position;

            _camera.GetComponent<CameraFollow>().SetTargetToFollow(playerCar);

            playerCar.GetComponent<CarController>()._inputManager = _inputManager;
        }

        void Start()
        {
            ShowErrorIfPlayerNotFound();
        }

        void Update()
        {

            if (_inputManager.inputLocked)
            {
                playerCar.GetComponent<CarController>().AddForceWhileInputLocked();
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

        void ShowErrorIfPlayerNotFound()
        {
            if (playerCar == null)
            {
                Debug.LogError("Player doesn't found");
            }
        }
    }
}
