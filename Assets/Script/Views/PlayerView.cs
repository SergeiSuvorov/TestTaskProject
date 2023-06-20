using Data;
using Interface;
using System;
using UniRx;
using UnityEngine;

namespace View
{
    public class PlayerView : MonoBehaviour,IDamageReceiver
    {
        [SerializeField] private Rigidbody2D  _rigidBody;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private WeaponView _weaponView;

        public WeaponView WeaponView => _weaponView;
        public Rigidbody2D Rigidbody => _rigidBody;

        public ReactiveCommand<int> GetDamage { get; private set; }
        public ReactiveCommand<IInventoryItem> OnTryTakeItem { get; private set; }

        private void Awake()
        {
            GetDamage = new ReactiveCommand<int>();
            OnTryTakeItem = new ReactiveCommand<IInventoryItem>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var item = collision.gameObject.GetComponent<ItemView>();
            if(item!=null)
            {
                OnTryTakeItem?.Execute(new Item(item, item.Amount));
                Destroy(item.gameObject);
            }
        }
    }
}