using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR
{
    [System.Serializable]
    public class PlayerData
    {
        public uint money;
        public byte selectedCarNumber;

        // TODO: add logic for road and gameMode
        public byte selectedRoadNumber;
        public byte selectedGameModeNumber;

        public PlayerData(PlayerSettings player)
        {
            money = player.playerMoney;
            selectedCarNumber = player.carPositionNumber;
        }

    }
}
