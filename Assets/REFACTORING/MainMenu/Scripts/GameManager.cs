using UnityEngine;

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

        }
    }
}
