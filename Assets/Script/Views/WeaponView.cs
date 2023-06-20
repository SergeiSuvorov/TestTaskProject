using Interface;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace View
{
    public class WeaponView : MonoBehaviour, IWeaponView, IAutoAttackZoneView
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Transform _targetPoint;

        private  ContactFilter2D _contactFilter;

        private IDamageReceiver _targetReceiver;

        public ReactiveCommand<IDamageReceiver> GetAttackContact { get; private set; }
        public ReactiveCommand<IDamageReceiver> LoseAttackContact { get; private set; }


        private void Awake()
        {
            LayerMask mask = gameObject.layer;

            _contactFilter = new ContactFilter2D();
            _contactFilter.SetLayerMask(mask);
            _contactFilter.useLayerMask = true;

            GetAttackContact = new ReactiveCommand<IDamageReceiver>();
            LoseAttackContact = new ReactiveCommand<IDamageReceiver>();
        }

        public void CheckAttackZone()
        {
            List<RaycastHit2D> hits = new List<RaycastHit2D>();

            Physics2D.Raycast(_targetPoint.position, _targetPoint.position - transform.position, _contactFilter, hits, 4);
            if (hits.Count == 0)
                LoseAttackContact?.Execute(_targetReceiver);

            for (int i = 0; i < hits.Count; i++)
            {
                if (!(hits[i].collider.isTrigger) && (hits[i].collider.TryGetComponent(out IDamageReceiver view)))
                {
                    GetAttackContact?.Execute(view);
                    _targetReceiver = view;
                }                    
            }
        }

        public IDamageReceiver Attack()
        {
            List<RaycastHit2D> hits = new List<RaycastHit2D>();

            Physics2D.Raycast(_targetPoint.position, _targetPoint.position - transform.position, _contactFilter, hits, 5);
            for(int i=0; i< hits.Count;i++)
            {
                if (!(hits[i].collider.isTrigger) && hits[i].collider.TryGetComponent(out IDamageReceiver view))
                    return view;
            }
            return null;
        }

        public void Init(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }
    }
}