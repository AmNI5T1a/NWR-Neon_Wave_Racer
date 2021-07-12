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
            Assets.OnSendItems += Create;
        }


        private void Create(object sender, Assets.OnSendItemsEventArgs e)
        {
            I_UI_ItemCreator creator = this.gameObject.GetComponent<I_UI_ItemCreator>();

            if (creator == null)
            {
                Debug.LogError("UI_ItemCreator script doesn't found class that realizes I_UI_ItemCreator interface...");
            }
            else
            {
                foreach (Assets.ItemAndStats<Car> car in e.cars_List)
                {
                    creator.CreateItem<Car>(car.item);
                }
            }

        }
    }
}
