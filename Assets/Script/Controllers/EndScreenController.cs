using Interface;
using Tools;
using UniRx;
using UnityEngine;
using View;

namespace Controller
{
    public sealed class EndScreenController : BaseController, IGameEndScreenController
    {
        private readonly ReactiveCommand _backToMainMenu = new ReactiveCommand();
        private readonly EndGameScreenView _view;
        public ReactiveCommand BackToMainMenu => _backToMainMenu;
        public EndScreenController(CompositeDisposable disposables, Transform placeForUi)
        {
            _view = LoadView< EndGameScreenView>(placeForUi, ResourcePathConstants.EndScreenResourcePath);
            _view.ButtonBackToMainMenu.OnClickAsObservable().Subscribe(_ => BackToMainMenu?.Execute()).AddTo(disposables);
        }
        public void OpenResultScreen(bool isWin)
        {
            _view.gameObject.SetActive(true);
            _view.SeeResultText(isWin);
        }
    }
}