using UnityEngine;

namespace NWR.Lobby
{
    public class UI_ShowList : MonoBehaviour
    {
        private bool isActive = false;
        public void ShowList()
        {
            this.gameObject.transform.parent.GetChild(1).gameObject.GetComponent<IShowList>().ShowList();
        }
    }
}
