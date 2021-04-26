using UnityEngine;

namespace NWR
{
    public enum ItemType
    {
        Item,
        Car,
        Road,
        GameMode
    }
    [System.Serializable]
    public class Item
    {
        [SerializeField] protected ItemType type;

        [Space(2)]

        [SerializeField] protected string name;

        [Space(2)]

        [SerializeField] protected byte positionNumber;

        [Space(2)]

        [SerializeField] protected Sprite sprite;

        [Space(5)]

        [SerializeField] protected bool thisItemSelected;

        public ItemType GetItemType() => type;
        public string GetName() => name;
        public byte GetPositionNumber() => positionNumber;

        public Sprite GetSprite() => sprite;

        public void ChangeSelectedStatus()
        {
            if (thisItemSelected)
            {
                thisItemSelected = false;
            }
            else
            {
                thisItemSelected = true;
            }
        }
    }
}
