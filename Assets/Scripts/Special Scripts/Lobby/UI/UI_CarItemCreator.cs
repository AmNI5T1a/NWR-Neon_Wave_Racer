using UnityEngine;
using NWR.Modules;

namespace NWR.Lobby
{
    public class UI_CarItemCreator : MonoBehaviour
    {
        void Awake()
        {
            Assets.OnSendCar += CreateItemAsUI_Gameobject;
        }

        private void CreateItemAsUI_Gameobject(Assets.ItemAndStats<Car> car)
        {
            Debug.Log(car.item.GetName());
        }
    }
}
