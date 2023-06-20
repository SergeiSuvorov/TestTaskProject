using Interface;
using Pathfinding;
using UniRx;
using UnityEngine;

namespace View
{
    public sealed class EnemyView : MonoBehaviour, IDamageReceiver, ISeekerView
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private EnemyAttackZone _enemyAttackZone;
        [SerializeField] private AIDestinationSetter _aIDestinationSetter;
        public AIDestinationSetter AIDestinationSetter=>_aIDestinationSetter;

        public EnemyAttackZone EnemyAttackZone => _enemyAttackZone;
        public bool isVisible => _spriteRenderer.isVisible;
        public ReactiveCommand<int> GetDamage { get; private set; }
        public ReactiveCommand<Transform> GetTarget { get; private set; }
     

        private void Awake()
        {
            GetDamage = new ReactiveCommand<int>();
            GetTarget = new ReactiveCommand<Transform>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.TryGetComponent(out PlayerView view);

            if (view != null)
            {
                GetTarget?.Execute(view.transform);
            }
        }
    }
}

