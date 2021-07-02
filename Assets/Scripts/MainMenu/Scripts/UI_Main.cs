using System;
using UnityEngine;

namespace NWR.MainMenu
{
    public class UI_Main : MonoBehaviour
    {
        public static event Action<int> onLoadLevel;
        void Start()
        {
            UI_Animations.Instance.Appear(this.gameObject);
        }

        public void ShowSettingsMenu()
        {
            UI_Animations.Instance.Hide(this.gameObject);
            UI_Animations.Instance.Appear(this.gameObject.transform.parent.gameObject.transform.GetChild(1).gameObject);
        }

        public void LoadLobby(int sceneId)
        {
            onLoadLevel?.Invoke(sceneId);
        }

    }
}
