using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR.MainMenu
{
    public class UI_Settings : MonoBehaviour
    {
        public void ShowMainMenu()
        {
            UI_Animations.Instance.Hide(this.gameObject);
            UI_Animations.Instance.Appear(this.gameObject.transform.parent.gameObject.transform.GetChild(0).gameObject);
        }
    }
}
