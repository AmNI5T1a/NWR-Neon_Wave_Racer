using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR
{
    public class RoadSpawner : MonoBehaviour
    {
        [Header("References: ")]
        [SerializeField] private List<GameObject> listOfRoads;

        [Header("In game: ")]
        [SerializeField] private List<GameObject> listOfInstanciatedRoads;

        void Start()
        {
            listOfInstanciatedRoads.Add(GameObject.Find("Bridge"));
        }
        public void SpawnNewRoad()
        {
            GameObject newRoad = GameObject.Instantiate(listOfRoads[0], listOfInstanciatedRoads[listOfInstanciatedRoads.Count - 1].transform.position + new Vector3(0f, 0f, 800f), listOfInstanciatedRoads[listOfInstanciatedRoads.Count - 1].transform.rotation);

            listOfInstanciatedRoads.Add(newRoad);

            if (listOfInstanciatedRoads.Count >= 3)
            {
                Destroy(listOfInstanciatedRoads[0]);
            }
        }
    }
}
