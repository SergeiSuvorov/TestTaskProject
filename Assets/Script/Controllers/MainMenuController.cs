using Model;
using Tools;
using Ui;
using UnityEngine;

namespace Controller
{
    public sealed class MainMenuController : BaseController
    {
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;

            _view = LoadView(placeForUi);
            _view.Init(StartGame);
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(ResourcePathConstants.MainMenuResourcePath), placeForUi, false);
            AddGameObjects(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame()
        {
            _profilePlayer.CurrentState.Value = GameState.Game;
        }
    }
}
