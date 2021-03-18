using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR
{

    public class Inventory
    {
        private List<Item> _listOfItems;

        public Inventory()
        {
            _listOfItems = new List<Item>();
        }

        public void AddItem(Item newItem)
        {
            _listOfItems.Add(newItem);
        }

        public List<Item> GetInventory() => _listOfItems;
    }
}
