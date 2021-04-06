using UnityEngine;

namespace NWR
{
    [System.Serializable]
    public class GameStyle : Item
    {
        [SerializeField] private bool boughtStatus;

        public bool WhetherItemWasPuchasedOrNot() => boughtStatus;
    }
}