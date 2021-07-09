namespace NWR.Modules
{
    [System.Serializable]
    public class DataToSaveAndLoad
    {
        public uint money;
        public ushort selectedCarID;
        public ushort selectedRoadID;
        public ushort selectedGameModeID;

        public int[] ID_OfAllPurchasedCars;
        public int[] ID_OfAllPurchasedRoads;

        public DataToSaveAndLoad()
        {
            money = Player.money;

            selectedCarID = Player.selectedCarID;
            selectedRoadID = Player.selectedRoadID;
            selectedGameModeID = Player.selectedGameModeID;

            ID_OfAllPurchasedCars = Player.boughtCars_List.ToArray();
            ID_OfAllPurchasedRoads = Player.boughtRoads_List.ToArray();
        }
    }
}