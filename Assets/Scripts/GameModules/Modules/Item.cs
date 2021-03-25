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
        public uint price;
        public int posNumber;
        public string name;

        public Sprite GetItemSprite()
        {
            switch (name)
            {
                case "Golf GTI": return ItemAssets.Instance.golfPreviewSprite;
                case "Subaru WRX": return ItemAssets.Instance.subaruPreviewSprite;

                default: return ItemAssets.Instance.unknownItemSprite;
            }
        }

        public GameObject GetCarAsGameObject()
        {
            switch (name)
            {
                case "Golf GTI": return ItemAssets.Instance.availableCarList[0];
                case "Subaru WRX": return ItemAssets.Instance.availableCarList[1];

                default: return null;
            }

        }
    }
}
