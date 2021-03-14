using UnityEngine;
using System.Collections.Generic;

namespace NWR
{


    public enum ControlMode
    {
        keyboard,
        touchpad
    }

    public class CarController : MonoBehaviour
    {
        [Header("References: ")]
        [SerializeField] private List<WheelCollider> _listOfWheelColliders;
        [SerializeField] private List<Transform> _listOfWheelTransforms;
        [SerializeField] private Rigidbody _carRigidbody;
        [SerializeField] private Transform centerOfMassTransformPosition;

        [Header("Settings: ")]

        [SerializeField] private float _horizontalInputSteeringSpeed;
        [SerializeField] private float steeringSpeed;
        [SerializeField] private float motorTorque;


        [Header("Stats in play mode: ")]
        [SerializeField] private float _currentSpeed;
        [SerializeField] private float _horizontalInput;
        [SerializeField] private float _verticalInput;


        void FixedUpdate()
        {
            // Calculate original speed of the car

            _currentSpeed = _carRigidbody.velocity.magnitude * 2.7f;


            CarSteering();
            UpdateWheelsPoses();
        }

        void CarSteering()
        {
            if (VirtualInputManager.Instance.MoveLeft && VirtualInputManager.Instance.MoveRight)
            {
                return;
            }

            if (!VirtualInputManager.Instance.MoveLeft && !VirtualInputManager.Instance.MoveRight)
            {
                _horizontalInput = Mathf.MoveTowards(_horizontalInput, 0f, _horizontalInputSteeringSpeed);

                this.transform.rotation = Quaternion.AngleAxis(_horizontalInput * steeringSpeed, Vector3.up);

                _listOfWheelColliders[0].steerAngle = _horizontalInput * steeringSpeed;
                _listOfWheelColliders[1].steerAngle = _horizontalInput * steeringSpeed;
            }

            if (VirtualInputManager.Instance.MoveLeft)
            {
                if (_horizontalInput >= -1f)
                {
                    _horizontalInput -= _horizontalInputSteeringSpeed;

                    this.transform.rotation = Quaternion.AngleAxis(_horizontalInput * steeringSpeed, Vector3.up);

                    _listOfWheelColliders[0].steerAngle = _horizontalInput * steeringSpeed;
                    _listOfWheelColliders[1].steerAngle = _horizontalInput * steeringSpeed;
                }
            }

            if (VirtualInputManager.Instance.MoveRight)
            {
                if (_horizontalInput <= 1f)
                {
                    _horizontalInput += _horizontalInputSteeringSpeed;

                    this.transform.rotation = Quaternion.AngleAxis(_horizontalInput * steeringSpeed, Vector3.up);

                    _listOfWheelColliders[0].steerAngle = _horizontalInput * steeringSpeed;
                    _listOfWheelColliders[1].steerAngle = _horizontalInput * steeringSpeed;
                }
            }

            if (VirtualInputManager.Instance.MoveFront)
            {
                _carRigidbody.AddForce(Vector3.forward * motorTorque);
            }
        }

        void UpdateWheelsPoses()
        {
            UpdateWheelsPose(_listOfWheelColliders[0], _listOfWheelTransforms[0]);
            UpdateWheelsPose(_listOfWheelColliders[1], _listOfWheelTransforms[1]);
            UpdateWheelsPose(_listOfWheelColliders[2], _listOfWheelTransforms[2]);
            UpdateWheelsPose(_listOfWheelColliders[3], _listOfWheelTransforms[3]);
        }

        void UpdateWheelsPose(WheelCollider collider, Transform transform)
        {
            Vector3 CurrentPosition = transform.position;
            Quaternion CurrentRotation = transform.rotation;

            collider.GetWorldPose(out CurrentPosition, out CurrentRotation);

            transform.position = CurrentPosition;
            transform.rotation = CurrentRotation;
        }
    }
}
