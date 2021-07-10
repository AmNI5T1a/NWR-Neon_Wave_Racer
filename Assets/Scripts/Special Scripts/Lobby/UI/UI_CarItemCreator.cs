using UnityEngine;
using NWR.Modules;
using TMPro;

namespace NWR.Lobby
{
    public class UI_CarItemCreator : MonoBehaviour
    {
        void Awake()
        {
            if (this.gameObject.activeSelf == false)
                this.gameObject.SetActive(true);
            Assets.OnSendCar += CreateItemAsUI_Gameobject;
        }

        private void CreateItemAsUI_Gameobject(Assets.ItemAndStats<Car> car)
        {
            var item_template_prefab = Resources.Load("Car item-template");
            GameObject item = Instantiate(item_template_prefab, this.gameObject.transform) as GameObject;
            item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = car.item.GetName();
        }
    }
}
