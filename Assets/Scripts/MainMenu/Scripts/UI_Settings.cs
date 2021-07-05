using UnityEngine;
using NWR.Modules;

namespace NWR.MainMenu
{
    public class UI_Settings : MonoBehaviour
    {
        IAppearAnimation appearAnimation = new Menu_Animations();
        IHideAnimation hideAnimation = new Menu_Animations();

        public void ShowMainMenu()
        {
            StartCoroutine(hideAnimation.HideAnimation(this.gameObject));
            StartCoroutine(appearAnimation.AppearAnimation(this.gameObject.transform.parent.GetChild(0).gameObject));
        }
    }
}
