namespace NWR.Modules
{
    public interface I_UI_ItemCreator
    {
        void CreateItem<T>(T item) where T : Item;
    }
}