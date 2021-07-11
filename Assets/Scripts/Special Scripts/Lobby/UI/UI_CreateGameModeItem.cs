using UnityEngine;
using NWR.Modules;

namespace NWR.Modules
{
    class UI_CreateGameModeItem : MonoBehaviour, I_UI_ItemCreator
    {
        public void CreateItem(Assets.OnFindPlayerSelectedItemsEventArgs e)
        {
            Debug.Log(this.gameObject.name + " object is trying to create game mode");
        }
    }
}