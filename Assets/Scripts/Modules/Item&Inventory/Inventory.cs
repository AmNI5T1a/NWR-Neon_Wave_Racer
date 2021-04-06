using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR
{

    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<Car> cars;

        [Space(2)]

        [SerializeField] private List<Road> roads;

        [Space(2)]

        [SerializeField] private List<GameStyle> gameStyles;

        public List<Car> GetListOfCars() => cars;
        public List<Road> GetListOfRoads() => roads;
        public List<GameStyle> GetListOfGameStyles() => gameStyles;
    }
}
