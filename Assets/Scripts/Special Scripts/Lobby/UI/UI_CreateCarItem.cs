using UnityEngine;
using NWR.Modules;

namespace NWR.Lobby
{
    class UI_CreateCarItem : MonoBehaviour, I_UI_ItemCreator
    {
        public void CreateItem(Assets.OnFindPlayerSelectedItemsEventArgs e)
        {
            Debug.Log(this.gameObject.name + " object is trying to create car");
        }
    }
}