using UnityEngine;

namespace NWR.Modules
{
    public class Item : ScriptableObject
    {
        [SerializeField] protected int ID;
        [SerializeField] protected string Denomination;

        public int GetID() => ID;
        public string GetName() => Denomination;
    }
}
