using UnityEngine;
using TMPro;
using NWR.Modules;


namespace NWR.Lobby
{
    public class UI_UpdateCarText : MonoBehaviour, I_UI_TextUpdated
    {
        public void UpdateText(Assets.OnSendPlayerSelectedItemsEventArgs e)
        {
            this.gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = e.playerCar.item.GetName();
        }
    }
}