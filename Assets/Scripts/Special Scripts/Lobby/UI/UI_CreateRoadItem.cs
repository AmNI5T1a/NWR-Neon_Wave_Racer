using UnityEngine;
using NWR.Modules;

namespace NWR.Lobby
{
    class UI_CreateRoadItem : MonoBehaviour, I_UI_ItemCreator
    {
        public void CreateItem<T>(T item) where T : Item
        {
            Debug.LogWarning("Not inmplemented");
        }
    }
}
