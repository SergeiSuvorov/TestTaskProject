using Interface;
using Tools;
using UniRx;
using UnityEngine;
using View;

namespace Controller
{
    public sealed class GameInputController : BaseController, IPlayerInputController, IGameMenuInputController
    {
        private readonly ReactiveProperty<Vector2> _moveInputProperty = new ReactiveProperty<Vector2>();
        private readonly ReactiveProperty<string> _ammoInfoProperty = new ReactiveProperty<string>();
        private readonly ReactiveCommand _openIventory = new ReactiveCommand();
        private readonly ReactiveCommand _shoot = new ReactiveCommand();
        private readonly ReactiveCommand _backToMainMenu = new ReactiveCommand();
         
        public ReactiveProperty<Vector2> MoveInputProperty => _moveInputProperty;
        public ReactiveProperty<string> AmmoInfoProperty => _ammoInfoProperty;
        public ReactiveCommand OpenIventory => _openIventory;
        public ReactiveCommand Shoot => _shoot;
        public ReactiveCommand BackToMainMenu => _backToMainMenu;
       

        private Vector2 _movingVector;

        private readonly GameInputView _view;

        public GameInputController(CompositeDisposable disposables, Transform placeForUi)
        {
            _view = LoadView<GameInputView>(placeForUi, ResourcePathConstants.GameMenuResourcePath); 
            
            _view.OpenInventoryButton.OnClickAsObservable().Subscribe(_ => OpenIventory?.Execute()).AddTo(disposables);
            _view.ShootButton.OnClickAsObservable().Subscribe(_=> {  Shoot?.Execute(); }).AddTo(disposables);
            _view.ButtonBackToMainMenu.OnClickAsObservable().Subscribe(_ => BackToMainMenu?.Execute()).AddTo(disposables);
            _ammoInfoProperty.Subscribe(value=> { _view.AmmoInfo.text = value; }).AddTo(disposables);

            Observable.EveryFixedUpdate().Subscribe(_ => { FixedUpdate(); }).AddTo(disposables);
        }

        private void FixedUpdate()
        {
            var horizontalInput = _view.Joystick.Horizontal;
            var verticalInput = _view.Joystick.Vertical;
            _movingVector = new Vector2(horizontalInput, verticalInput);
            MoveInputProperty.Value = _movingVector;
        }
    }
}