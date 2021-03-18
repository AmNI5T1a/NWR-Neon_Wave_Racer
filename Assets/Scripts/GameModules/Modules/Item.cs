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
            GameStyle
        }

        public ItemType itemType;
        public int amount;
    }
}
