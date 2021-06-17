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
        public byte[] purcahsedCarsID;

        public byte selectedRoadID;
        public byte[] purchasedRoadsID;

        public byte selectedGameModeID;

        public PlayerData(PlayerSettings player)
        {
            money = player.money;

            selectedCarID = player.selectedCarID;
            purcahsedCarsID = player.purchasedCarsIDs.ToArray();

            selectedRoadID = player.selectedRoadID;
            purchasedRoadsID = player.purchasedRoadsID.ToArray();

            selectedGameModeID = player.selectedGameStyleID;
        }

    }
}
