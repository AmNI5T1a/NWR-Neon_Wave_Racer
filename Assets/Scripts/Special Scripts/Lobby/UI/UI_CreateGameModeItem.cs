using UnityEngine;
using NWR.Modules;

namespace NWR.Modules
{
    class UI_CreateGameModeItem : MonoBehaviour, I_UI_ItemCreator
    {
        public void CreateItem<T>(T item) where T : Item
        {
            throw new System.NotImplementedException();
        }
    }
}