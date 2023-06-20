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
        private readonly ProfilePlayer _profilePlayer;
        private readonly SaveDataRepository _saveDataRepository;

        private BaseController _currentController;

        public MainController(Transform placeForUi)
        {
            _placeForUi = placeForUi;
            _disposables = new CompositeDisposable();

            _saveDataRepository = new SaveDataRepository();
          
            _profilePlayer = new ProfilePlayer();
            _profilePlayer.LoadData(_saveDataRepository);

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
            _profilePlayer.SaveData(_saveDataRepository);

            _currentController?.Dispose();

            switch (state)
            {
                case GameState.None:
                    break;
                case GameState.Start:
                    _currentController = new MainMenuController(_placeForUi, _profilePlayer);
                    break;
                case GameState.Game:
                    _currentController = new GameController(_placeForUi, _profilePlayer);
                    break;
            }
        }
    }
}
