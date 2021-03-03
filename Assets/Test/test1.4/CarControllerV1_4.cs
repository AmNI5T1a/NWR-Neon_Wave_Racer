using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR
{
    public class CarControllerV1_4 : MonoBehaviour
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
        [SerializeField] private float turnSpeed = 15;
        [SerializeField] private float speed = 10;
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
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
        }

        void Steer()
        {
            _steeringAngle = maxSteerAngle * _horizontalInput;

            _wheelColliders[0].steerAngle = _steeringAngle;
            _wheelColliders[1].steerAngle = _steeringAngle;

            _carRigidbody.velocity += new Vector3(_horizontalInput, 0f, 0f);
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
                _wheelColliders[0].motorTorque = motorForce;
                _wheelColliders[1].motorTorque = motorForce;
                _wheelColliders[2].motorTorque = motorForce;
                _wheelColliders[3].motorTorque = motorForce;
            }
            else
            {
                _wheelColliders[0].motorTorque = 0;
                _wheelColliders[1].motorTorque = 0;
                _wheelColliders[2].motorTorque = 0;
                _wheelColliders[3].motorTorque = 0;
            }

            if (VirtualInputManager.Instance.Brake)
            {
                _wheelColliders[0].motorTorque = 0.0001f;
                _wheelColliders[1].motorTorque = 0.0001f;
                _wheelColliders[2].motorTorque = 0.0001f;
                _wheelColliders[3].motorTorque = 0.0001f;
                _wheelColliders[0].brakeTorque = brakeForce;
                _wheelColliders[1].brakeTorque = brakeForce;
                _wheelColliders[2].brakeTorque = brakeForce;
                _wheelColliders[3].brakeTorque = brakeForce;
            }
            else
            {
                _wheelColliders[0].brakeTorque = 0;
                _wheelColliders[1].brakeTorque = 0;
                _wheelColliders[2].brakeTorque = 0;
                _wheelColliders[3].brakeTorque = 0;

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
    }
}
