using UnityEngine;
using NWR.Modules;

namespace NWR.Lobby
{
    public class LobbyManager : MonoBehaviour
    {
        public static LobbyManager Instance;

        [Header("Stats: ")]
        [SerializeField] public Vector3 playerCarPosition;

        [Header("Play mode stats:")]
        [SerializeField] public GameObject playerCar;


        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public void InstanciatePlayerCar(Car playerCar)
        {
            GameObject car = Instantiate(playerCar.GetCarAsGameObject(), playerCarPosition, Quaternion.identity);
        }
    }
}
