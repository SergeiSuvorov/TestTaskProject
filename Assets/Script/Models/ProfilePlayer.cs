using Data;
using Interface;
using System.Collections.Generic;
using Tools;
using UniRx;

namespace Model
{
    public class ProfilePlayer
    {
        public readonly int PlayerSpeed = 2;
        public readonly int PlayerMaxHealth = 20;
        public readonly string PlayerName = "Player";

        public List<IInventoryItem> InventoryItems;
        public IWeaponItem PlayerEquippedWeapon;

        public ReactiveProperty<GameState> CurrentState { get; }

        public ProfilePlayer()
        {
            CurrentState = new ReactiveProperty<GameState>();
        }     

        public void SaveData(SaveDataRepository saveDataRepository)
        {
            var saveData = new SavedData();
            saveData.SetItems(InventoryItems);
            saveData.SetWeapon(PlayerEquippedWeapon);
            saveDataRepository.Save(saveData);
        }

        public void LoadData(SaveDataRepository saveDataRepository)
        {
            var loadData = saveDataRepository.Load();
            InventoryItems = loadData?.GetItems();
            PlayerEquippedWeapon = loadData?.GetWeapon();
        }
    }
}
