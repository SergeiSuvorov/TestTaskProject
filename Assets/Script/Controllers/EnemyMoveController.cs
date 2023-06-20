using Pathfinding;
using UniRx;
using UnityEngine;
using View;

namespace Controller
{
    public sealed class EnemyMoveController:BaseController
    {
        private readonly EnemyView _view;
        private readonly AIDestinationSetter _aIDestinationSetter;
        private readonly CompositeDisposable _enemyDisposables;
        private readonly Vector3 _leftScale = new Vector3(1, 1, 1);
        private readonly Vector3 _rightScale = new Vector3(-1, 1, 1);

        private int _movingDirectionXIndex;
        private Vector2 _lastFramePosition;

        public EnemyMoveController(EnemyView view, CompositeDisposable enemyDisposables)
        {
            _view = view;
            _enemyDisposables = enemyDisposables;
            _movingDirectionXIndex = (int)_view.transform.localScale.x;

            _aIDestinationSetter = _view.AIDestinationSetter;
            _view.GetTarget.Subscribe(target => { SetSeekerTarget(target); }).AddTo(_enemyDisposables);
            Observable.EveryFixedUpdate().Subscribe(_ => { Update(); }).AddTo(_enemyDisposables);
        }

        private void Update()
        {
            var positionXChange = _view.transform.position.x - _lastFramePosition.x;

            if (positionXChange * _movingDirectionXIndex < 0)
            {
                EnemyTurn();
                _movingDirectionXIndex *= -1;
            }

            _lastFramePosition = _view.transform.position;
        }

        private void EnemyTurn()
        {
            if (_movingDirectionXIndex > 0)
                _view.transform.localScale = _rightScale;
            else
                _view.transform.localScale = _leftScale;
        }

        private void SetSeekerTarget(Transform target)
        {
            _aIDestinationSetter.target = target;
        }
    }
}