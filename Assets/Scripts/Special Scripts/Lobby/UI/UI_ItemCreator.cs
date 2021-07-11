using UnityEngine;
using NWR.Modules;
using TMPro;

namespace NWR.Lobby
{
    [RequireComponent(typeof(I_UI_ItemCreator))]
    public class UI_ItemCreator : MonoBehaviour
    {
        void Awake()
        {
            Assets.OnFindPlayerSelectedItems += Create;
        }


        private void Create(object sender, Assets.OnFindPlayerSelectedItemsEventArgs e)
        {
            I_UI_ItemCreator creator = this.gameObject.GetComponent<I_UI_ItemCreator>();

            if (creator == null)
            {
                Debug.LogError("UI_ItemCreator script doesn't found class that realizes I_UI_ItemCreator inteface...");
            }
            else
                creator.CreateItem(e);

        }
    }
}
