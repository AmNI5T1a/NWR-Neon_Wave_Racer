using System.Collections.Generic;
using UnityEngine;

namespace NWR.Modules
{
    [System.Serializable]
    public class Assets : MonoBehaviour
    {
        [System.Serializable]
        private class Cars
        {
            [SerializeField] private Car car;
            [SerializeField] private bool isBought;
        }

        [SerializeField] private List<Cars> listOfCars;

        [System.Serializable]
        private class Roads
        {
            [SerializeField] private Road road;
            [SerializeField] private bool isBought;
        }

        [SerializeField] private List<Roads> listOfRoads;
        [SerializeField] private List<GameMode> listOfGameModes;


        void Awake()
        {
            Player.onGetIDsBoughtCars += UpdateListOfCarsAfterDeserialization;
            Player.onGetIDsBoughtRoads += UpdateListOfRoadsAfterDeserialization;
        }

        private void UpdateListOfCarsAfterDeserialization(List<int> IDs_OfBoughtCars)
        {
            Debug.Log("Trying to update list of cars and set which was bought in previous sessions");
        }
        private void UpdateListOfRoadsAfterDeserialization(List<int> IDs_ofBoughtRoads)
        {
            Debug.Log("Trying to update list of roads and set which was bought in previous sessions");
        }
    }
}
