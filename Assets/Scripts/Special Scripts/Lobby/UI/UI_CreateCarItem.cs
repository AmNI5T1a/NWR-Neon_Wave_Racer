using UnityEngine;
using NWR.Modules;
using TMPro;

namespace NWR.Lobby
{
    class UI_CreateCarItem : MonoBehaviour, I_UI_ItemCreator
    {
        public void CreateItem<T>(T item) where T : Item
        {
            var item_template_prefab = Resources.Load("Car item-template");
            GameObject obj = Instantiate(item_template_prefab, this.gameObject.transform) as GameObject;
            obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.GetName();
        }
    }
}