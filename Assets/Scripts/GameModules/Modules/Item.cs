using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR
{
    public class Item
    {
        public enum ItemType
        {
            Road,
            GameStyle,
            Car
        }

        public ItemType itemType;
        public int amount;
        public bool boughtStatus;
        public int price;
        public int posNumber;
        public string name;
    }
}
