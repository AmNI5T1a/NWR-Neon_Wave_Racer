using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR
{
    public class KeyboardInput : MonoBehaviour
    {
        void Update()
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
        }
    }
}
