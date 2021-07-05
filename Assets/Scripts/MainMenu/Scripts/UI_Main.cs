using System;
using UnityEngine;
using NWR.Modules;

namespace NWR.MainMenu
{
    public class UI_Main : MonoBehaviour
    {
        IAppearAnimation appearAnimation = new Menu_Animations();
        IHideAnimation hideAnimation = new Menu_Animations();
        void Start()
        {
            StartCoroutine(appearAnimation.AppearAnimation(this.gameObject));
        }

        public void ShowSettingsMenu()
        {
            StartCoroutine(hideAnimation.HideAnimation(this.gameObject));
            StartCoroutine(appearAnimation.AppearAnimation(this.gameObject.transform.parent.GetChild(1).gameObject));
        }

        public void LoadLobby(int sceneId)
        {
            LevelLoader.Instance.LoadScene(sceneId);
        }

    }
}
