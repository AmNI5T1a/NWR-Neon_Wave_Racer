using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NWR
{
    public class UIAnimationManager : MonoBehaviour
    {
        [SerializeField] private GameObject _mainMenuUIComponent;
        [SerializeField] private GameObject _settingsUIComponent;

        public void ShowSettingsMenu()
        {
            _mainMenuUIComponent.GetComponent<Animator>().SetTrigger("Hide");
            _settingsUIComponent.GetComponent<Animator>().SetTrigger("Show");
        }

        public void ShowMainMenu()
        {
            _mainMenuUIComponent.GetComponent<Animator>().SetTrigger("Show");
            _settingsUIComponent.GetComponent<Animator>().SetTrigger("Hide");
        }
    }
}
