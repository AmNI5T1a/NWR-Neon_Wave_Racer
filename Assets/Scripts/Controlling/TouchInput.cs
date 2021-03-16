using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor.UI;

namespace NWR
{
    public class TouchInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public void MoveFrontInput(bool status)
        {
            VirtualInputManager.Instance.MoveFront = status;

        }

        public void MoveBackInput(bool status)
        {
            VirtualInputManager.Instance.MoveBack = status;
        }

        public void MoveLeftSideInput(bool status)
        {
            VirtualInputManager.Instance.MoveLeft = status;
        }

        public void MoveRightSideInput(bool status)
        {
            VirtualInputManager.Instance.MoveRight = status;
        }


        public void BrakesInput(bool status)
        {
            VirtualInputManager.Instance.Brake = status;
        }


        public void OnPointerDown(PointerEventData eventData)
        {

        }

        public void OnPointerUp(PointerEventData eventData)
        {

        }
    }
}
