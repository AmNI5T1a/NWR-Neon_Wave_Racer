using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR
{
    public class CarController_TEST : MonoBehaviour
    {
        [Header("References: ")]
        [SerializeField] private Rigidbody _carRigidbody;

        [Header("Stats: ")]
        [SerializeField] private float speed;
        [SerializeField] private float turnSpeed;
        [SerializeField] private float maxSpeed;

        void FixedUpdate()
        {
            if (VirtualInputManager.Instance.MoveFront && VirtualInputManager.Instance.MoveBack)
                return;

            CheckAndNormalizeSpeed();

            if (VirtualInputManager.Instance.MoveFront)
            {
                _carRigidbody.AddRelativeForce(Vector3.forward * speed);
            }

            if (VirtualInputManager.Instance.MoveBack)
            {
                _carRigidbody.AddRelativeForce(-Vector3.forward * speed);
            }

            if (VirtualInputManager.Instance.MoveLeft)
            {
                _carRigidbody.MovePosition(transform.position + -transform.right * turnSpeed * Time.fixedDeltaTime);
            }

            if (VirtualInputManager.Instance.MoveRight)
            {
                _carRigidbody.MovePosition(transform.position + transform.right * turnSpeed * Time.fixedDeltaTime);
            }
        }

        void CheckAndNormalizeSpeed()
        {
            if (_carRigidbody.velocity.magnitude > maxSpeed)
            {
                _carRigidbody.velocity = _carRigidbody.velocity.normalized * maxSpeed;
            }
        }

    }
}
