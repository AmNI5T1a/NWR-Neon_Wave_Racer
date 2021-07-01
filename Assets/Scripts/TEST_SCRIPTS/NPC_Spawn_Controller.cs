using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR
{
    public class NPC_Spawn_Controller : MonoBehaviour
    {
        [Header("References: ")]
        [SerializeField] private GameObject _player;
        [SerializeField] private List<GameObject> listOfNPC_Cars;

        [Space(10)]

        [Header("Stats: ")]
        [SerializeField] private byte minNPC_CarsOnScene;
        [SerializeField] private byte maxNPC_CarsOnScene;

        [Space(2)]

        [SerializeField] private byte startSpawnNPC_from;
        [SerializeField] private byte startSpawnNPC_to;

        [Space(2)]

        [SerializeField] private List<float> listOfSpawnPositions;

        [Space(2)]

        [SerializeField] private float zSpawnPositionStarts_from;
        [SerializeField] private float zSpawnPositionStarts_to;

        [Header("In game stats: ")]

        // ! Change 'SerializeField' to 'HideInTheInspector' after final field test
        [SerializeField] private List<GameObject> listOfSpawnedNPC_Cars;
        private void Start()
        {
            StartCoroutine(SpawnCar());
        }

        private void Update()
        {

        }

        private IEnumerator SpawnCar()
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(startSpawnNPC_from, startSpawnNPC_to));

            if (listOfSpawnedNPC_Cars.Count <= minNPC_CarsOnScene)
            {
                byte randomCarNumberFromList = Convert.ToByte(UnityEngine.Random.Range(0, (listOfNPC_Cars.Count - 1)));
                byte randomXPositionToSpawnNPC = Convert.ToByte(UnityEngine.Random.Range(0, (listOfSpawnPositions.Count - 1)));
                float randomZPositionToSpawnNPC = UnityEngine.Random.Range(zSpawnPositionStarts_from, zSpawnPositionStarts_to);

                Vector3 npcSpawnPosition = new Vector3(0, 0, 0);

                GameObject newNPC = Instantiate(listOfNPC_Cars[randomCarNumberFromList], npcSpawnPosition, Quaternion.identity);
            }
        }
    }
}