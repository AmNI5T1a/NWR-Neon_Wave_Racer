using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR
{
    public class CarController : MonoBehaviour
    {
        [Header("References: ")]
        [SerializeField] private List<WheelCollider> _listOfWheelColliders;
        [SerializeField] private List<Transform> _listOfWheelTransforms;

        [Header("Stats: ")]
        [SerializeField] private float motorForce;
        [SerializeField] private float breakForce;
        [SerializeField] private float MaxSteerAngle;

        private float currentbreakForce;
        private float currentSteerAngle;
        private float horizontalInput;
        private float verticalInput;
        private void FixedUpdate()
        {
            GetInput();
            HandleMotor();
            HandleSteering();
            UpdateWheels();
        }

        void GetInput()
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
        }
        void HandleMotor()
        {
            if (VirtualInputManager.Instance.MoveFront && VirtualInputManager.Instance.MoveBack)
                return;

            if (VirtualInputManager.Instance.MoveLeft && VirtualInputManager.Instance.MoveRight)
                return;

            if (VirtualInputManager.Instance.MoveFront)
            {
                _listOfWheelColliders[2].motorTorque = motorForce;
                _listOfWheelColliders[3].motorTorque = motorForce;
            }

            if (VirtualInputManager.Instance.MoveBack)
            {
                _listOfWheelColliders[2].motorTorque = -1 * motorForce / 1.3f;
                _listOfWheelColliders[3].motorTorque = -1 * motorForce / 1.3f;
            }

            currentbreakForce = VirtualInputManager.Instance.Brake ? breakForce : 0f;

            if (VirtualInputManager.Instance.Brake)
            {
                ApplyBreaking();
            }
        }

        void ApplyBreaking()
        {
            _listOfWheelColliders[0].brakeTorque = currentbreakForce;
            _listOfWheelColliders[1].brakeTorque = currentbreakForce;
            _listOfWheelColliders[2].brakeTorque = currentbreakForce;
            _listOfWheelColliders[3].brakeTorque = currentbreakForce;
        }

        void HandleSteering()
        {
            currentSteerAngle = MaxSteerAngle * horizontalInput;

            _listOfWheelColliders[0].steerAngle = currentSteerAngle;
            _listOfWheelColliders[1].steerAngle = currentSteerAngle;
        }

        void UpdateWheels()
        {
            UpdateSingleWheel(_listOfWheelColliders[0], _listOfWheelTransforms[0]);
            UpdateSingleWheel(_listOfWheelColliders[1], _listOfWheelTransforms[1]);
            UpdateSingleWheel(_listOfWheelColliders[2], _listOfWheelTransforms[2]);
            UpdateSingleWheel(_listOfWheelColliders[3], _listOfWheelTransforms[3]);
        }

        void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
        {
            Vector3 pos;
            Quaternion rot;
            wheelCollider.GetWorldPose(out pos, out rot);

            wheelTransform.rotation = rot;
            wheelTransform.position = pos;
        }
    }
}