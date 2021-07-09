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
            Player.OnSetPlayerCarInLobby += CreateOrUpdateCarAsGameObject;
        }

        private void CreateOrUpdateCarAsGameObject(Car playerCar)
        {
            LobbyManager.playerCar = Instantiate(playerCar.GetCarAsGameObject());
            Debug.LogWarning("Created car in lobby");
        }
    }
}
