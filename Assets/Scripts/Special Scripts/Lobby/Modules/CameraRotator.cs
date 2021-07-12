using System.Collections;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [Range(0f, 0.15f)]
    [SerializeField] private float _rotationSpeed;

    [Range(0f, 0.040f)]
    [SerializeField] private float _delayBetweenRotations;
    void Start()
    {
        StartCoroutine(RotateCamera());
    }

    private IEnumerator RotateCamera()
    {
        while (true)
        {
            transform.Translate(Vector3.right * _rotationSpeed);
            this.gameObject.transform.LookAt(this.gameObject.transform.parent.transform);
            yield return new WaitForSeconds(_delayBetweenRotations);
        }
    }
}
