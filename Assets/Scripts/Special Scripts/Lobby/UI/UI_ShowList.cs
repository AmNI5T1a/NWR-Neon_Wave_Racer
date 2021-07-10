using UnityEngine;

namespace NWR.Lobby
{
    public class UI_ShowList : MonoBehaviour
    {
        public void ShowList()
        {
            this.gameObject.transform.parent.GetChild(0).gameObject.GetComponent<IShowOrHideList>().ShowOrHide();
        }
    }
}
