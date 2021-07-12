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

            Assets.OnSendPlayerSelectedItems += InstanciatePlayerCar;
        }

        public void InstanciatePlayerCar(object sender, Assets.OnSendPlayerSelectedItemsEventArgs e)
        {
            GameObject car = Instantiate(e.playerCar.item.GetCarAsGameObject(), playerCarPosition, Quaternion.identity);
        }
    }
}
