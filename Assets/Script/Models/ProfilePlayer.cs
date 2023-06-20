using Interface;
using System.Collections.Generic;
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
    }
}
