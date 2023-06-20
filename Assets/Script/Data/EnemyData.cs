using Interface;
using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public sealed class EnemyData : IEnemyModel
    {
        [SerializeField] private string _enemyId;
        [SerializeField] private string _enemyName;
        [SerializeField] private int _health;
        [SerializeField] private int _enemySpeed;
        [SerializeField] private int _damage;
        [SerializeField] private int _maxAmmoAmount;
        [SerializeField] private float _attackDelay;
        [SerializeField] private float _reloadTime;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _prefabPath;

        public int Health => _health;
        public int EnemySpeed => _enemySpeed;
        public Sprite Sprite => _sprite;
        public int MaxAmmoAmount => _maxAmmoAmount;
        public int Damage => _damage;
        public float AttackDelay => _attackDelay;
        public float ReloadTime => _reloadTime;
        public string PrefabPath => _prefabPath;
        public string EnemyId => _enemyId;
        public string EnemyName => _enemyName;
    }
}
