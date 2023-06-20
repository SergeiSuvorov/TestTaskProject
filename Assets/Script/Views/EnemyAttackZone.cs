using Interface;
using UniRx;
using UnityEngine;

namespace View
{
    public class EnemyAttackZone : MonoBehaviour, IAutoAttackZoneView
    {
        public ReactiveCommand<IDamageReceiver> GetAttackContact { get; private set; }
        public ReactiveCommand<IDamageReceiver> LoseAttackContact { get; private set; }

        private void Awake()
        {
            GetAttackContact = new ReactiveCommand<IDamageReceiver>();
            LoseAttackContact = new ReactiveCommand<IDamageReceiver>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.TryGetComponent(out PlayerView view);
            GetAttackContact?.Execute(view);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerView view))
            {
                LoseAttackContact?.Execute(view);
            }
        }
    }
}

