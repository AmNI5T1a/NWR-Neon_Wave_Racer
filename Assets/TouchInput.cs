using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor.UI;

namespace NWR
{


    public class TouchInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        bool selectStatus = false;
        void Update()
        {
            ShowDebugLog();
        }

        public void SelectButtonDown(bool status)
        {
            this.selectStatus = status;
        }

        private void ShowDebugLog()
        {
            if (selectStatus == true)
            {
                Debug.LogWarning("Input working...");
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            selectStatus = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            selectStatus = false;
        }
    }
}
