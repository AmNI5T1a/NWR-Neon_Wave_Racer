using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR
{


    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Vector3 distanceBetweenCameraAndObjectToFollow;
        [SerializeField] private Transform targetToFollow;

        public void SetTargetToFollow(GameObject target)
        {
            targetToFollow = target.transform;
        }
        void Update()
        {
            Vector3 cameraPosition = targetToFollow.position + distanceBetweenCameraAndObjectToFollow;

            this.transform.position = cameraPosition;
        }
    }
}
