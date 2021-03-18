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
        [SerializeField] private byte currentReplacingRoad = 0;
        [SerializeField] private Transform lastAddedRoadTransform;

        void Start()
        {
            listOfInstanciatedRoads.Add(GameObject.Find("Bridge"));
        }
        public void SpawnNewRoad()
        {
            if (listOfInstanciatedRoads.Count <= 2)
            {
                GameObject newRoad = GameObject.Instantiate(listOfRoads[0], listOfInstanciatedRoads[listOfInstanciatedRoads.Count - 1].transform.position + new Vector3(0f, 0f, 800f), listOfInstanciatedRoads[listOfInstanciatedRoads.Count - 1].transform.rotation);
                listOfInstanciatedRoads.Add(newRoad);

                lastAddedRoadTransform = newRoad.transform;
            }
            else
            {
                listOfInstanciatedRoads[currentReplacingRoad].transform.position = lastAddedRoadTransform.position + new Vector3(0f, 0f, 800f);
                lastAddedRoadTransform = listOfInstanciatedRoads[currentReplacingRoad].transform;

                currentReplacingRoad++;

                if (currentReplacingRoad == 3)
                    currentReplacingRoad = 0;
            }

        }
    }
}
