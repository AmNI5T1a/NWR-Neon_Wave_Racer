using UnityEngine;
using NWR.Modules;

namespace NWR.Lobby
{
    public class LobbyManager : MonoBehaviour
    {
        [SerializeField] static public Vector3 playerCarPosition;
        [SerializeField] static public GameObject playerCar;


        void Awake()
        {
            if (this.gameObject.activeSelf == false)
                this.gameObject.SetActive(true);
            Player.OnSetPlayerCarInLobby += CreateOrUpdateCarAsGameObject;
        }

        private void CreateOrUpdateCarAsGameObject(Car playerCar)
        {
            LobbyManager.playerCar = Instantiate(playerCar.GetCarAsGameObject());
            Debug.LogWarning("Created car in lobby");
        }
    }
}
