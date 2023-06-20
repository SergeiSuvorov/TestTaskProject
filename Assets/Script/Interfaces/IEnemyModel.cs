using UnityEngine;

namespace Interface
{
    public interface IEnemyModel: IWeaponInfo
    {
        string EnemyName { get; }
        string EnemyId { get; }
        int Health { get; }
        int EnemySpeed { get; }
        Sprite Sprite { get; }
        string PrefabPath { get; }
    }
}