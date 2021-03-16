using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NWR
{
    public enum MovementInput { Keyboard = 1, Touch = 2 };
    public class InputManager : MonoBehaviour
    {
        [Header("Global Settings:")]
        [SerializeField] public MovementInput input;
        [SerializeField] private bool touchCanvasStatus = false;
        [SerializeField] public bool inputLocked = false;


        [Header("References: ")]
        [SerializeField] private GameObject touchInput;
        [SerializeField] private GameObject touchPad_Canvas;

        void Start()
        {
            touchInput.SetActive(false);
            touchPad_Canvas.SetActive(false);
        }
        void Update()
        {
            if (input == MovementInput.Keyboard && inputLocked == false)
                KeyboardInput();
            else if (input == MovementInput.Touch && inputLocked == false)
                TouchInput();
        }

        void KeyboardInput()
        {
            if (Input.GetKey(KeyCode.D))
            {
                VirtualInputManager.Instance.MoveRight = true;
            }
            else
            {
                VirtualInputManager.Instance.MoveRight = false;
            }

            if (Input.GetKey(KeyCode.A))
            {
                VirtualInputManager.Instance.MoveLeft = true;
            }
            else
            {
                VirtualInputManager.Instance.MoveLeft = false;
            }

            if (Input.GetKey(KeyCode.W))
            {
                VirtualInputManager.Instance.MoveFront = true;
            }
            else
            {
                VirtualInputManager.Instance.MoveFront = false;
            }

            if (Input.GetKey(KeyCode.S))
            {
                VirtualInputManager.Instance.MoveBack = true;
            }
            else
            {
                VirtualInputManager.Instance.MoveBack = false;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                VirtualInputManager.Instance.Brake = true;
            }
            else
            {
                VirtualInputManager.Instance.Brake = false;
            }

            if (Input.GetKey(KeyCode.C))
            {
                VirtualInputManager.Instance.C = true;
            }
            else
            {
                VirtualInputManager.Instance.C = false;
            }
        }

        void TouchInput()
        {
            if (touchCanvasStatus == false)
            {
                touchInput.SetActive(true);
                touchPad_Canvas.SetActive(true);
            }
        }
    }
}
