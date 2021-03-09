using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR
{
    public class CarController : MonoBehaviour
    {
        [Header("In game stats:")]
        [SerializeField] private float _horizontalInput;
        [SerializeField] private float _verticalInput;
        [SerializeField] private float _steeringAngle;

        [Header("References: ")]
        [SerializeField] private List<WheelCollider> _wheelColliders;
        [SerializeField] private List<Transform> _wheelTransforms;
        [SerializeField] private Rigidbody _carRigidbody;

        [Header("Stats: ")]
        [SerializeField] private float motorForce = 50f;
        [SerializeField] private float brakeForce = 50f;
        [SerializeField] private float maxSteerAngle = 30;
        [SerializeField] private float maxSpeed = 100;

        void FixedUpdate()
        {
            GetHorizontalAndVerticalInput();
            Steer();
            Accelerate();
            UpdateWheelsPoses();

        }

        void GetHorizontalAndVerticalInput()
        {
            if (VirtualInputManager.Instance.MoveLeft && VirtualInputManager.Instance.MoveRight)
            {
                return;
            }
            if (VirtualInputManager.Instance.MoveLeft)
            {
                _horizontalInput = -1f;
            }
            else if (VirtualInputManager.Instance.MoveRight)
            {
                _horizontalInput = 1f;
            }
            else
            {
                _horizontalInput = 0f;
            }
            // _horizontalInput = Input.GetAxis("Horizontal");
            // _verticalInput = Input.GetAxis("Vertical");
        }

        void Steer()
        {
            _steeringAngle = maxSteerAngle * _horizontalInput;

            _wheelColliders[0].steerAngle = _steeringAngle;
            _wheelColliders[1].steerAngle = _steeringAngle;
        }

        void Accelerate()
        {
            if (VirtualInputManager.Instance.MoveFront && VirtualInputManager.Instance.Brake)
                return;

            if (_carRigidbody.velocity.magnitude > maxSpeed)
            {
                _carRigidbody.velocity = _carRigidbody.velocity.normalized * maxSpeed;
            }


            if (VirtualInputManager.Instance.MoveFront)
            {
                foreach (WheelCollider wheel in _wheelColliders)
                    wheel.motorTorque = motorForce;
            }
            else
            {
                foreach (WheelCollider wheel in _wheelColliders)
                    wheel.motorTorque = 0f;
            }

            if (VirtualInputManager.Instance.Brake)
            {
                foreach (WheelCollider wheel in _wheelColliders)
                    wheel.motorTorque = 0f;

                foreach (WheelCollider wheel in _wheelColliders)
                    wheel.brakeTorque = brakeForce;
            }
            else
            {
                foreach (WheelCollider wheel in _wheelColliders)
                    wheel.brakeTorque = 0;

            }
        }

        void UpdateWheelsPoses()
        {
            UpdateWheelsPose(_wheelColliders[0], _wheelTransforms[0]);
            UpdateWheelsPose(_wheelColliders[1], _wheelTransforms[1]);
            UpdateWheelsPose(_wheelColliders[2], _wheelTransforms[2]);
            UpdateWheelsPose(_wheelColliders[3], _wheelTransforms[3]);
        }

        void UpdateWheelsPose(WheelCollider collider, Transform transform)
        {
            Vector3 CurrentPosition = transform.position;
            Quaternion CurrentRotation = transform.rotation;

            collider.GetWorldPose(out CurrentPosition, out CurrentRotation);

            transform.position = CurrentPosition;
            transform.rotation = CurrentRotation;
        }




        // TouchInput

        public void HorizontalTouchInput(float horizontal)
        {
            _horizontalInput = horizontal;

            SteerTouchInput(_horizontalInput);
        }

        public void VericalTouchInput(float vertical)
        {
            _verticalInput = vertical;

            SteerTouchInput(_verticalInput);
        }

        public void SteerTouchInput(float horizontalTouchInput)
        {
            _steeringAngle = maxSteerAngle * horizontalTouchInput;

            for (byte c = 0; c <= 1; c++)
            {
                _wheelColliders[c].steerAngle = _steeringAngle;
            }
        }

        public void FrontMoveTouchInput(bool status)
        {
            if (status == true)
            {
                foreach (WheelCollider wheel in _wheelColliders)
                    wheel.motorTorque = motorForce;
                Debug.Log("Adding force to the wheels");
            }
            else if (status == false)
            {
                foreach (WheelCollider wheel in _wheelColliders)
                    wheel.motorTorque = 0f;

                Debug.LogWarning("Stop adding force to the wheels");
            }
        }

        public void BrakeTouchInput(bool status)
        {
            if (status == true)
            {
                foreach (WheelCollider wheel in _wheelColliders)
                    wheel.motorTorque = 0f;

                foreach (WheelCollider wheel in _wheelColliders)
                    wheel.brakeTorque = brakeForce;
            }
            else if (status == false)
            {
                foreach (WheelCollider wheel in _wheelColliders)
                    wheel.brakeTorque = 0;
            }
        }
    }
}
