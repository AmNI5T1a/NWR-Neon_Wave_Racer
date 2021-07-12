using UnityEngine;
using TMPro;
using NWR.Modules;

namespace NWR.Lobby
{
    public class UI_UpdateRoadText : MonoBehaviour, I_UI_TextUpdated
    {
        public void UpdateText(Assets.OnSendPlayerSelectedItemsEventArgs e)
        {
            this.gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = e.playerRoad.item.GetName();
        }
    }
}