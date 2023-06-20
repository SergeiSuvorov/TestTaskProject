using Data;
using Interface;
using Model;
using System.Collections.Generic;
using Tools;
using UniRx;
using UnityEngine;

namespace Controller
{
    sealed class MainController : BaseController
    {
        private readonly CompositeDisposable _disposables;
        private readonly Transform _placeForUi;
        private readonly Transform _parentGameTransform;

        private readonly ProfilePlayer _profilePlayer;
        private readonly SaveDataRepository _saveDataRepository;
        private readonly ItemManagerController _itemManagerController;

        private BaseController _currentController;

        public MainController(Transform placeForUi, Transform parentGameTransform)
        {
            _placeForUi = placeForUi;
            _parentGameTransform = parentGameTransform;
            _disposables = new CompositeDisposable();

            _saveDataRepository = new SaveDataRepository();
            _itemManagerController = new ItemManagerController(_parentGameTransform);
            AddController(_itemManagerController);
            _profilePlayer = new ProfilePlayer();
            SetLoadData();

            _profilePlayer
                .CurrentState
                .Subscribe(value => { OnChangeGameState(value); })
                .AddTo(_disposables);
            
            _profilePlayer.CurrentState.Value = GameState.Start;           
        }

        protected override void OnDispose()
        {
            _currentController?.Dispose();
            _disposables.Clear();

            base.OnDispose();
        }

        private void OnChangeGameState(GameState state)
        {
            SaveData();

            _currentController?.Dispose();

            switch (state)
            {
                case GameState.None:
                    break;
                case GameState.Start:
                    _currentController = new MainMenuController(_placeForUi, _profilePlayer);
                    break;
                case GameState.Game:
                    _currentController = new GameController(_placeForUi,_parentGameTransform, _profilePlayer, _itemManagerController);
                    break;
            }
        }

        private void SetLoadData()
        {
            var loadData = _saveDataRepository.Load();
            List<IInventoryItem> inventoryItems = new List<IInventoryItem>();
            var idsList = loadData?.ItemDataIds;
            if (idsList?.Count>0)
                for(int i=0; i< idsList.Count;i++)
                {
                    var item = new Item(_itemManagerController.GetItem(idsList[i]));
                    inventoryItems.Add(item);
                }
            _profilePlayer.InventoryItems = inventoryItems;

            if(loadData?.PlayerEquippedWeaponId>0)
            {
                var weaponRecords = _itemManagerController.GetItem(loadData.PlayerEquippedWeaponId) as WeaponDataRecord;
                _profilePlayer.PlayerEquippedWeapon = new WeaponItem(weaponRecords, true, 1);
            }
        }

        private void SaveData()
        {
            var saveData = new SavedData();
            saveData.SetItems(_profilePlayer.InventoryItems,_profilePlayer.PlayerEquippedWeapon);
            _saveDataRepository.Save(saveData);
        }
    }
}
