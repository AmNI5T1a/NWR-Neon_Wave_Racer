using UnityEngine;

namespace NWR.Modules
{
    [CreateAssetMenu(fileName = "Car", menuName = "NWR/Items/Car")]
    public class Car : Item
    {
        [SerializeField] private uint price;
        [SerializeField] private string description;
        [SerializeField] private GameObject prefab;
    }
}
