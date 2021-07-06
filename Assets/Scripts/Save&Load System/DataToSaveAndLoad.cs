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

        public DataToSaveAndLoad(Player playersData)
        {
            money = playersData.money;

            selectedCarID = playersData.selectedCarID;
            selectedRoadID = playersData.selectedRoadID;
            selectedGameModeID = playersData.selectedGameModeID;

            ID_OfAllPurchasedCars = playersData.boughtCars_List.ToArray();
            ID_OfAllPurchasedRoads = playersData.boughtRoads_List.ToArray();
        }

        public DataToSaveAndLoad()
        {
            this.money = 0;
            this.selectedCarID = 0;
            this.selectedRoadID = 0;
            this.selectedGameModeID = 0;

            this.ID_OfAllPurchasedCars = new int[1] { 0 };
            this.ID_OfAllPurchasedRoads = new int[0];
        }
    }
}