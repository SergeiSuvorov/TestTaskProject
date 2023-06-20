using Data;
using Interface;
using Model;
using Tools;
using UniRx;
using UnityEngine;
using View;

namespace Controller
{
    public sealed class GameController : BaseController
    {
        private readonly CompositeDisposable _gameDisposables;
        private readonly ProfilePlayer _profilePlayer;
        private readonly Transform _parentGameTransform;

        private readonly IGameMenuInputController _gameMenuInputController;
        private readonly IGameEndScreenController _gameEndScreenController;

        public GameController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _gameDisposables = new CompositeDisposable();
            _profilePlayer = profilePlayer;

            _parentGameTransform = CreateParentGameObject();

            var mapView = LoadView<MapView>(_parentGameTransform, ResourcePathConstants.MapResourcePath);
            var xSize = mapView.MapSize.x / (mapView.CellSize.x * 2);
            var ySize = mapView.MapSize.y / (mapView.CellSize.y * 2);
            
            var inputController = new GameInputController(_gameDisposables, placeForUi);
            _gameMenuInputController = inputController;
            _gameMenuInputController.BackToMainMenu.Subscribe(_ => { BackToMainMenu(); }).AddTo(_gameDisposables);
            AddController(inputController);

            var viewServices = new ViewServices();
            AddController(viewServices);

            var gameEndScreenController = new EndScreenController(_gameDisposables, placeForUi);
            _gameEndScreenController = gameEndScreenController;
            _gameEndScreenController.BackToMainMenu.Subscribe(_ => { BackToMainMenu(); }).AddTo(_gameDisposables);
            AddController(gameEndScreenController);

            var itemManager = new ItemManagerController(_parentGameTransform);
            AddController(itemManager);

            if (_profilePlayer.PlayerEquippedWeapon == null)
                _profilePlayer.PlayerEquippedWeapon = new WeaponItem(itemManager.GetItem(GameConstants.StartWeaponId) as WeaponDataRecord,true,1);
            var playerController = new PlayerContoller(_profilePlayer, _parentGameTransform, placeForUi, inputController, xSize, ySize);
            playerController.OnDeathAction.Subscribe(value=> { OnGameEnd(false); }).AddTo(_gameDisposables);
            AddController(playerController);

            var cameraController = new CameraController(playerController.PlayerGameObject, _gameDisposables, xSize, ySize);
            AddController(cameraController);

            var healthBarManagerController = new HealthBarManagerController(viewServices, placeForUi);
            healthBarManagerController.SetHealthBarToObject(playerController);
            AddController(healthBarManagerController);

            var enemyManagerController = new EnemyManagerController(_parentGameTransform, xSize, ySize, GameConstants.StartEnemyCount, viewServices);
            enemyManagerController.onEnemyCreate.Subscribe(value => { healthBarManagerController.SetHealthBarToObject(value); }).AddTo(_gameDisposables);
            enemyManagerController.onEnemyDied.Subscribe(value => { itemManager.CreateRandomItemInCoordinate(value); }).AddTo(_gameDisposables);
            enemyManagerController.onLastEnemyDied.Subscribe(value => { OnGameEnd(true); }).AddTo(_gameDisposables);
            enemyManagerController.CreateEnemys();
            AddController(enemyManagerController);
        }
        protected override void OnDispose()
        {
            _gameDisposables.Clear();
            base.OnDispose();
        }
        private Transform CreateParentGameObject()
        {
            var objectView = new GameObject("GameRoot");
            AddGameObjects(objectView);

            return objectView.transform;
        }
        private void OnGameEnd(bool isWin)
        {
            _gameEndScreenController.OpenResultScreen(isWin);
        }
        private void BackToMainMenu()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
        }
    }
 }