using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR
{


    public class LobbyCameraRotator : MonoBehaviour
    {
        [Header("Settings: ")]
        [SerializeField] private float cameraSpeedRotation;
        void Update()
        {
            this.transform.Rotate(0, cameraSpeedRotation * Time.deltaTime, 0);
        }
    }
}
