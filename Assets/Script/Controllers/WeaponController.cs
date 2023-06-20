using Interface;
using UniRx;
using UnityEngine;
using View;

namespace Controller
{
    public sealed class WeaponController : BaseController, IWeapon, IDamageDealer
    {
        private float currentTime;
        private IWeaponView _view;
        private IWeaponInfo _model;
        private Transform _playerTransform;
        private IDamageReceiver _targetReceiver;
        private ReactiveProperty<string> _ammoInfoProperty;

        public int Damage => _model.Damage;
        public float AttackDelay => _model.AttackDelay;
        public float ReloadTime => _model.AttackDelay;
        public int MaxAmmoAmount => _model.MaxAmmoAmount;
        public int AmmoAmount { get; private set; }
        public bool IsReadyToAttack { get; private set; }
        public bool IsReloading { get; private set; }

        public WeaponController(IWeaponView view, IShootInputController inputWeapon, IWeaponInfo weaponInfo, CompositeDisposable disposables, Transform player)
        {
            _view = view;
            _model = weaponInfo;
            _ammoInfoProperty = inputWeapon.AmmoInfoProperty;
            _playerTransform = player;

            var attackZone = (_view as WeaponView);
            attackZone?.GetAttackContact.Subscribe(SetAttackTarget).AddTo(disposables);
            attackZone?.LoseAttackContact.Subscribe(LoseAttackTarget).AddTo(disposables);

            Observable
                .EveryUpdate()
                .Subscribe(_ => 
                {
                    attackZone.CheckAttackZone();
                    Update(); 
                })
                .AddTo(disposables);


            inputWeapon.Shoot.Subscribe(_ => { Attack(); }).AddTo(disposables);

            IsReadyToAttack = true;
            IsReloading = false;
            AmmoAmount = _model.MaxAmmoAmount;
            _ammoInfoProperty.Value = $"{AmmoAmount} / {MaxAmmoAmount}";
        }

        public void Reload()
        {
            IsReloading = true;
            _ammoInfoProperty.Value = "Reloading";
            currentTime = ReloadTime;
        }

        private void Update()
        {
            UpdateDelayTimer();
            if (_targetReceiver != null)
                Attack();
        }

        private void UpdateDelayTimer()
        {
            if (IsReadyToAttack)
                return;

            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                return;
            }
            else
            {
                IsReadyToAttack = true;
                if (IsReloading)
                {
                    IsReloading = false;
                    AmmoAmount = MaxAmmoAmount;
                    _ammoInfoProperty.Value = $"{AmmoAmount} / {MaxAmmoAmount}";
                }
            }

        }

        private void SetAttackTarget(IDamageReceiver target)
        {
                _targetReceiver = target;
        }

        private void LoseAttackTarget(IDamageReceiver target)
        {
                _targetReceiver = null;
        }

        public void Attack()
        {
            if (!IsReadyToAttack)
                return;
            var attackResult = _view.Attack();

            if (attackResult != null)
            {
                SetDamage(attackResult);
                (attackResult as ISeekerView).GetTarget?.Execute(_playerTransform);
            }

            IsReadyToAttack = false;
            AmmoAmount--;

            if (AmmoAmount > 0)
            {
                currentTime = AttackDelay;
                _ammoInfoProperty.Value = $"{AmmoAmount} / {MaxAmmoAmount}";
            }
            else
                Reload();

        }
        public void SetDamage(IDamageReceiver person)
        {
            person.GetDamage?.Execute(Damage);
        }
    }
}