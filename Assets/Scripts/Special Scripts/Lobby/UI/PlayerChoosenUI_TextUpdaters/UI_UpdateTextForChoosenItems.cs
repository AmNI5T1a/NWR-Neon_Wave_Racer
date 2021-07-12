using UnityEngine;
using NWR.Modules;

namespace NWR.Lobby
{
    [RequireComponent(typeof(I_UI_TextUpdated))]
    public class UI_UpdateTextForChoosenItems : MonoBehaviour
    {
        void Awake()
        {
            Assets.OnSendPlayerSelectedItems += SetNewText;
        }
        public void SetNewText(object sender, Assets.OnSendPlayerSelectedItemsEventArgs e)
        {
            this.gameObject.GetComponent<I_UI_TextUpdated>().UpdateText(e);
        }
    }
}
