using UnityEngine;

namespace NWR.Lobby
{
    public class UI_List : MonoBehaviour, IShowList
    {
        public void ShowList()
        {
            Debug.Log("Showing u and animating: " + this.gameObject.name);
            ActivateOrDeactivateObject();

        }
        private void ActivateOrDeactivateObject()
        {
            if (this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                this.gameObject.SetActive(true);
            }
        }
    }
}
