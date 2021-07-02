using UnityEngine;
using NWR.Modules;

namespace NWR.MainMenu
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager _instance;

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }

        void Start()
        {
            UI_Main.onLoadLevel += LevelLoad;
        }

        private void LevelLoad(int sceneId)
        {
            LevelLoader.Instance.LoadScene(sceneIdToLoad: 2);
        }
    }
}
