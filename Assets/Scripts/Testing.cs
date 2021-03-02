using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR
{
    public class Testing : MonoBehaviour
    {
        public float speed;

        void Update()
        {
            if (VirtualInputManager.Instance.MoveRight && VirtualInputManager.Instance.MoveLeft)
                return;

            if (VirtualInputManager.Instance.MoveLeft)
            {
                this.gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
                this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            if (VirtualInputManager.Instance.MoveRight)
            {
                this.gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
                this.gameObject.transform.rotation = Quaternion.Euler(0f, -180f, 0f);
            }
        }
    }
}