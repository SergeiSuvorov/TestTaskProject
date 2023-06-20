using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = nameof(EnemysData), menuName = "EnemyData/" + nameof(EnemysData))]
    public sealed class EnemysData : ScriptableObject
    {
        [SerializeField] private List<EnemyData> _enemysData;
        public List<EnemyData> EnemyDatas => _enemysData;
    }
}
