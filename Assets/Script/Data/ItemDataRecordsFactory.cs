namespace Data
{
    public static class ItemDataRecordsFactory
    {
        public static BaseItemDataRecord GetItemDataRecords(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.None:
                    return null; 
                case ItemType.Item:
                    return new ItemDataRecord(); 
                case ItemType.Weapon:
                    return new WeaponDataRecord();
                default:
                    return null;
            }
        }
    }
}
