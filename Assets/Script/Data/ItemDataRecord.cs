using System;

namespace Data
{
    [Serializable]
    public class ItemDataRecord : BaseItemDataRecord
    {
        public ItemDataRecord():base(ItemType.Item)
        {
            _canEquipped = false;            
        }
    }
}
