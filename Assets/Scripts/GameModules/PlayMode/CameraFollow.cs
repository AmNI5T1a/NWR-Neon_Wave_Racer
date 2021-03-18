using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR
{


    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private float cameraSpeed = 10.0f;
        [SerializeField] private Vector3 distanceBetweenCameraAndObjectToFollow;
        [SerializeField] private Transform targetToFollow;

        void Update()
        {
            Vector3 cameraPosition = targetToFollow.position + distanceBetweenCameraAndObjectToFollow;

            this.transform.position = cameraPosition;
        }
    }
}