using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR
{
    [System.Serializable]
    public class PlayerData
    {
        public uint money;

        public byte selectedCarID;

        public PlayerData(PlayerSettings player)
        {
            money = player.playerMoney;
            selectedCarID = player.playerSelectedCarID;
        }

    }
}
