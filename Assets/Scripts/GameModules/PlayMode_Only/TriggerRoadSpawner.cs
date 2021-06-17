using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NWR
{
    public class TriggerRoadSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _roadSpawner;

        void Start()
        {
            _roadSpawner = GameObject.Find("RoadSpawner");
        }

        void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.tag == "CarCollider")
            {
                _roadSpawner.GetComponent<RoadSpawner>().SpawnNewRoad();
            }
        }
    }
}
