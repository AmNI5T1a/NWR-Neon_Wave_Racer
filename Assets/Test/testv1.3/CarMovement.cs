using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR
{
    public class CarMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _carRigidbody;
        [SerializeField] private Transform _carTransform;

        [SerializeField] private float speed;
        [SerializeField] private float maxSpeed;

        [SerializeField] private float turnSpeed;



        [SerializeField] private float _horizontalInput;
        [SerializeField] private float _verticalInput;


        void Update()
        {
            GetInput();
            Move();
        }

        void GetInput()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
        }
        void Move()
        {
            if (VirtualInputManager.Instance.MoveFront && VirtualInputManager.Instance.MoveBack)
                return;

            if (_carRigidbody.velocity.magnitude > maxSpeed)
            {
                _carRigidbody.velocity = _carRigidbody.velocity.normalized * maxSpeed;
            }

            if (VirtualInputManager.Instance.MoveFront)
            {
                _carRigidbody.AddRelativeForce(Vector3.forward * _verticalInput * speed * Time.deltaTime);
            }

            if (VirtualInputManager.Instance.MoveBack)
            {
                _carRigidbody.AddRelativeForce(-Vector3.forward * -_verticalInput * speed * Time.deltaTime);
            }

            if (VirtualInputManager.Instance.MoveLeft)
            {
                _carTransform.Rotate(new Vector3(0f, _horizontalInput, 0f));
            }

            if (VirtualInputManager.Instance.MoveRight)
            {
                _carTransform.Rotate(new Vector3(0f, _horizontalInput, 0f));
            }
        }
    }
}
