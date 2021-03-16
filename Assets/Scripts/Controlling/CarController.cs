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
        [SerializeField] private InputManager _inputManager;

        [Header("Settings: ")]

        [SerializeField] private float _horizontalInputSteeringSpeed;
        [SerializeField] private float steeringSpeed;
        [SerializeField] private float motorTorque;
        [SerializeField] private float brakeTorque;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float minSpeed;

        [SerializeField] private float[] gears;
        [SerializeField] private float minRPM;
        [SerializeField] private float maxRPM;


        [Header("Stats in play mode: ")]
        [SerializeField] private float _horizontalInput;
        [SerializeField] private float _verticalInput;

        [Space(10)]

        [SerializeField] private float _currentSpeed;
        [SerializeField] private int currentSpeedGear;
        [SerializeField] private float currentRPM;
        [SerializeField] private float shiftDownCooldown;
        [SerializeField] private bool shiftDownOnCooldown;
        void Start()
        {
            currentSpeedGear = 0;

            shiftDownOnCooldown = false;
        }


        void FixedUpdate()
        {
            // Calculate original speed of the car

            _currentSpeed = _carRigidbody.velocity.magnitude * 2.7f;

            if (_inputManager.inputLocked == false)
            {
                SpeedController();
                CarSteering();
                CarMovement();
                GearsSystem();
            }

            UpdateWheelsPoses();
        }

        void SpeedController()
        {
            if (_carRigidbody.velocity.magnitude > maxSpeed)
            {
                _carRigidbody.velocity = _carRigidbody.velocity.normalized * maxSpeed;
            }

            if (_carRigidbody.velocity.magnitude <= minSpeed && _inputManager.inputLocked == false)
            {
                _carRigidbody.velocity = _carRigidbody.velocity.normalized * minSpeed;
            }
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
                if (_horizontalInput > -1f)
                {
                    _horizontalInput -= _horizontalInputSteeringSpeed;

                    this.transform.rotation = Quaternion.AngleAxis(_horizontalInput * steeringSpeed, Vector3.up);

                    _listOfWheelColliders[0].steerAngle = _horizontalInput * steeringSpeed;
                    _listOfWheelColliders[1].steerAngle = _horizontalInput * steeringSpeed;
                }
            }

            if (VirtualInputManager.Instance.MoveRight)
            {
                if (_horizontalInput < 1f)
                {
                    _horizontalInput += _horizontalInputSteeringSpeed;

                    this.transform.rotation = Quaternion.AngleAxis(_horizontalInput * steeringSpeed, Vector3.up);

                    _listOfWheelColliders[0].steerAngle = _horizontalInput * steeringSpeed;
                    _listOfWheelColliders[1].steerAngle = _horizontalInput * steeringSpeed;
                }
            }
        }

        void CarMovement()
        {

            if (VirtualInputManager.Instance.MoveFront && VirtualInputManager.Instance.Brake)
                return;

            if (VirtualInputManager.Instance.MoveFront && !VirtualInputManager.Instance.Brake)
            {
                foreach (WheelCollider wheel in _listOfWheelColliders)
                    wheel.motorTorque = motorTorque * gears[currentSpeedGear];
            }
            else
            {
                foreach (WheelCollider wheel in _listOfWheelColliders)
                    wheel.motorTorque = 0f;
            }

            if (VirtualInputManager.Instance.Brake && !VirtualInputManager.Instance.MoveFront)
            {
                foreach (WheelCollider wheel in _listOfWheelColliders)
                {
                    wheel.motorTorque = 0f;
                    wheel.brakeTorque = brakeTorque;
                }

            }
            else
            {
                foreach (WheelCollider wheel in _listOfWheelColliders)
                    wheel.brakeTorque = 0f;
            }
        }

        void GearsSystem()
        {
            currentRPM = Mathf.Abs((_listOfWheelColliders[0].rpm / 4) * gears[currentSpeedGear]);

            ///          
            /// Shift Up
            ///
            if (currentRPM > maxRPM && VirtualInputManager.Instance.MoveFront && currentSpeedGear <= 3)
            {
                currentSpeedGear++;
            }

            /// 
            /// Shift Down
            ///
            if (currentRPM < minRPM && currentSpeedGear != 0 && !shiftDownOnCooldown)
            {
                currentSpeedGear--;
                shiftDownCooldown = 1f;
                shiftDownOnCooldown = true;
            }
            else if (currentRPM < minRPM && currentSpeedGear != 0 && shiftDownOnCooldown)
            {
                shiftDownCooldown -= Time.deltaTime;
                if (shiftDownCooldown <= 0)
                {
                    shiftDownOnCooldown = false;
                }
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

        public void AddForceWhileInputLocked()
        {
            foreach (WheelCollider wheel in _listOfWheelColliders)
                wheel.motorTorque = 5000f;
        }
    }
}
