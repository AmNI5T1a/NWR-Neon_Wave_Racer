using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR
{
    public class MainMenuAnimations : MonoBehaviour
    {
        [Header("References: ")]
        [SerializeField] private GameObject _mainMenuUIComponent;
        [SerializeField] private GameObject _settingsUIComponent;

        [Space(10)]

        [SerializeField] private GameObject _levelLoader;



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

        public void PlayButtonPressed()
        {
            // TODO: 1. hide menu UI component
            // TODO: 2. show black screen (start transaction)
            // TODO: 2. Start load lobby
            // TODO: 3. after lobby loaded, load player save
            // TODO: 4. hide black screen (end transaction with lobby on screen)

            // * 1
            _mainMenuUIComponent.GetComponent<Animator>().SetTrigger("Hide");

            // * 2
            _levelLoader.GetComponent<LevelLoader>().LoadScene(currentSceneId: 0, needToLoadSceneId: 1);
        }
    }
}
