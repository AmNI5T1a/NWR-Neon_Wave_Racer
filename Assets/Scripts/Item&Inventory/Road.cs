using UnityEngine;

namespace NWR.Modules
{
    [CreateAssetMenu(fileName = "Road", menuName = "NWR/Items/Road")]
    public class Road : Item
    {
        [SerializeField] private uint price;
        [SerializeField] private string description;
        [SerializeField] private Sprite image;
    }
}
