using Interface;
using Model;
using System.Linq;
using Tools;
using UniRx;
using UnityEngine;
using View;

namespace Controller
{
    public sealed class PlayerContoller : BaseController, IGetHealthBar
    {
        private readonly ProfilePlayer _profilePlayer;
        private readonly PlayerView _view;
        private readonly float _xSize;
        private readonly float _ySize;
        private readonly CompositeDisposable _playerDisposables;
        private readonly Vector3 _leftScale = new Vector3(1, 1, 1);
        private readonly Vector3 _rightScale = new Vector3(-1, 1, 1);

        private IInventory _inventoryController;
        private int _movingDirectionXIndex;
        private Vector2 _movingVector;

        public Vector3 Position => _view.transform.position;
        public bool isVisible => true;
        public string Name => _profilePlayer.PlayerName;
        public int MaxHealth => _profilePlayer.PlayerMaxHealth;
        public GameObject PlayerGameObject => _view.gameObject;
        public ReactiveProperty<int> HealthValue { get; private set; }

        public ReactiveCommand<IGetHealthBar> OnDeathAction { get; private set; }

        public PlayerContoller(ProfilePlayer profilePlayer, Transform parentGameTransform, Transform placeForUi, IPlayerInputController playerInputController , float xSize, float ySize)
        {
            _playerDisposables = new CompositeDisposable();

            _profilePlayer = profilePlayer;

            _xSize = xSize- GameConstants.MapForPlayerMoveOffset;
            _ySize = ySize- GameConstants.MapForPlayerMoveOffset;

            OnDeathAction = new ReactiveCommand<IGetHealthBar>();
            HealthValue = new ReactiveProperty<int>();
            HealthValue.Value = _profilePlayer.PlayerMaxHealth;


            _view = LoadView<PlayerView>(parentGameTransform,ResourcePathConstants.PlayerResourcePath);
            _view.OnTryTakeItem.Subscribe(TryAddItemTo).AddTo(_playerDisposables);
            _view.GetDamage.Subscribe(GetDamage).AddTo(_playerDisposables);
            
            _movingDirectionXIndex = (int)_view.transform.localScale.x;
            var inventoryController = new InventoryController(placeForUi, profilePlayer, playerInputController, _playerDisposables);
            AddController(inventoryController);

            _inventoryController = inventoryController;

            var weaponController = new WeaponController(_view.WeaponView, playerInputController, profilePlayer.PlayerEquippedWeapon, _playerDisposables, _view.transform) ;
            _view.WeaponView.Init(profilePlayer.PlayerEquippedWeapon.SpriteIcon);
            AddController(weaponController);

            playerInputController.MoveInputProperty.Subscribe(value => { _movingVector = value;}).AddTo(_playerDisposables);

            Observable.EveryFixedUpdate().Subscribe(_ => { PlayerMove(_movingVector);}).AddTo(_playerDisposables);
        }

        private void PlayerMove(Vector2 movingVector)
        {
           
            if (movingVector.x * _movingDirectionXIndex<0)
            {
                PayerTurn();
                _movingDirectionXIndex *= -1;
            }

            var position = _view.Rigidbody.position + movingVector * _profilePlayer.PlayerSpeed * Time.fixedDeltaTime;

            position = new Vector2(Mathf.Clamp(position.x, -_xSize, _xSize), Mathf.Clamp(position.y, -_ySize, _ySize));

            _view.Rigidbody.MovePosition(position);
        }
        private void PayerTurn()
        {
            if (_movingDirectionXIndex>0)
                _view.transform.localScale = _rightScale;
            else
                _view.transform.localScale = _leftScale;
        }
        private void TryAddItemTo(IInventoryItem item)
        {
            _inventoryController.TryToAdd(item);
            _profilePlayer.InventoryItems = _inventoryController.GetAllItems().ToList();
        }
        private void GetDamage(int damage)
        {
            HealthValue.Value -= damage;

            if (HealthValue.Value < 0)
                OnDeathAction?.Execute(this);
        }
        protected override void OnDispose()
        {
            _playerDisposables.Clear();
            base.OnDispose();
        }
    }
}