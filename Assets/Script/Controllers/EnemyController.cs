using Interface;
using System;
using UniRx;
using UnityEngine;
using View;

namespace Controller
{
    public sealed class EnemyController : BaseController, IGetHealthBar 
    {
        private readonly IEnemyModel _model;
        private readonly EnemyView _view;
        public readonly CompositeDisposable EnemyDisposables;
        public GameObject EnemyGameObject => _view.gameObject;
        public string EnemyId => _model.EnemyId;
        public Vector3 Position => _view.transform.position;
        public bool isVisible => _view.isVisible;
        public string Name => _model.EnemyName;
        public int MaxHealth => _model.Health;

        public ReactiveProperty<int> HealthValue { get; private set; }
        public ReactiveCommand<IGetHealthBar> OnDeathAction { get; private set; }

        public EnemyController(Transform parentGameTransform, EnemyView view, IEnemyModel model)
        {
            _view = view;
            _model = model;

            EnemyDisposables = new CompositeDisposable();
            var enemyMoveController = new EnemyMoveController(_view, EnemyDisposables);
            AddController(enemyMoveController);

            var enemyAttackController = new EnemyAttackController(_view, _model, EnemyDisposables);
            AddController(enemyAttackController);

            _view.transform.parent = parentGameTransform;
            _view.gameObject.SetActive(false);

            _view.GetDamage.Subscribe(damage => { GetDamage(damage); }).AddTo(EnemyDisposables);

            OnDeathAction = new ReactiveCommand<IGetHealthBar>();
            HealthValue = new ReactiveProperty<int>();
            HealthValue.Value = _model.Health;
        }


        private void GetDamage(int damage)
        {
            HealthValue.Value -= damage;

            if (HealthValue.Value < 0)
                OnDeathAction?.Execute(this);
        }

        public void ActivateEnemyInCoordinate(Vector2 coordinate)
        {
            _view.transform.position = coordinate;
            _view.gameObject.SetActive(true);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            EnemyDisposables.Clear();
        }
    }
}