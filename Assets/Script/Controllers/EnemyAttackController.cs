using Interface;
using UniRx;
using UnityEngine;
using View;

namespace Controller
{
    public sealed class EnemyAttackController : BaseController, IAutoAttackWeapon
    {
        private readonly CompositeDisposable _enemyDisposables;
        private readonly IEnemyModel _model;
        private readonly EnemyView _view;

        private float _currentTime;
        private IDamageReceiver _targetReceiver;
   
        public GameObject EnemyGameObject => _view.gameObject;
        public string EnemyId => _model.EnemyId;
        public Vector3 Position => _view.transform.position;
        public bool isVisible => _view.isVisible;
        public string Name => _model.EnemyName;
        public int MaxHealth => _model.Health;
        public int Damage => _model.Damage;
        public float AttackDelay => _model.AttackDelay;
        public float ReloadTime => _model.AttackDelay;
        public int MaxAmmoAmount => _model.MaxAmmoAmount;
        public int AmmoAmount { get; private set; }
        public bool IsReadyToAttack { get; private set; }
        public bool IsReloading { get; private set; }

        public EnemyAttackController(EnemyView view, IEnemyModel model, CompositeDisposable enemyDisposables)
        {
            _view = view;
            _model = model;

            _enemyDisposables = enemyDisposables;

            _view.EnemyAttackZone.GetAttackContact.Subscribe(SetAttackTarget).AddTo(_enemyDisposables);
            _view.EnemyAttackZone.LoseAttackContact.Subscribe(LoseAttackTarget).AddTo(_enemyDisposables);

            IsReadyToAttack = true;
            IsReloading = false;
            AmmoAmount = _model.MaxAmmoAmount;

            Observable.EveryFixedUpdate().Subscribe(_ => { Update(); }).AddTo(_enemyDisposables);
        }

        private void Update()
        {
            UpdateDelayTimer();

            if (_targetReceiver != null)
                SetDamage(_targetReceiver);
        }

        private void UpdateDelayTimer()
        {
            if (IsReadyToAttack)
                return;

            if (_currentTime > 0)
            {
                _currentTime -= Time.deltaTime;
                return;
            }
            else
            {
                IsReadyToAttack = true;
                if (IsReloading)
                {
                    IsReloading = false;
                    AmmoAmount = MaxAmmoAmount;
                }
            }
        }

        private void SetAttackTarget(IDamageReceiver target)
        {
            if (_targetReceiver == null)
                _targetReceiver = target;
        }

        private void LoseAttackTarget(IDamageReceiver target)
        {
            if (_targetReceiver == target)
                _targetReceiver = null;
        }


        public void Reload()
        {
            IsReloading = true;
            _currentTime = ReloadTime;
        }

        public void SetDamage(IDamageReceiver receiver)
        {

            if (!IsReadyToAttack)
                return;

            receiver.GetDamage?.Execute(Damage);

            IsReadyToAttack = false;
            AmmoAmount--;

            if (AmmoAmount > 0)
            {
                _currentTime = AttackDelay;
            }
            else
                Reload();
        }
    }
}