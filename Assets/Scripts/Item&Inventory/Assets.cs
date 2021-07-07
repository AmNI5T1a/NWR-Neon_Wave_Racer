using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NWR.Modules
{
    public class Assets : MonoBehaviour
    {
        [System.Serializable]
        public class StackOfItems<T> where T : Item
        {
            [SerializeField] private T _item;
            [SerializeField] public bool _isBought;

            public T GetItem
            {
                get { return _item; }
            }
        }

        [SerializeField] public List<StackOfItems<Car>> stackOf_Cars;
        [SerializeField] public List<StackOfItems<Road>> stackOf_Roads;


        void Start()
        {

        }


        private void SetIfCarPurchased(List<int> IDs_ofBoughtItems)
        {

        }
    }
}
