using UnityEngine;
using TMPro;
using NWR.Modules;

namespace NWR.Lobby
{
    public class UI_UpdateGameModeText : MonoBehaviour, I_UI_TextUpdated
    {
        public void UpdateText(Assets.OnSendPlayerSelectedItemsEventArgs e)
        {
            TextMeshProUGUI text = this.gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();

            if (text != null)
            {
                text.text = e.playerGameMode.item.GetName();
            }
            else
            {
                Debug.Log("This gameObject doesn't contain TextMeshProUGUI");
            }
        }
    }
}