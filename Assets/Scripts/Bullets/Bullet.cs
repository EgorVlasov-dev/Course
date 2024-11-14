using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet> BulletOff;
        
        [SerializeField]
        private int _damage;

        [SerializeField]
        private Rigidbody2D _rigidbody2D;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField] 
        private float MagnitudeVelocity = 3;
        
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetVelocity(Vector2 velocity)
        {
            _rigidbody2D.velocity = velocity.normalized * MagnitudeVelocity;
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<Entity>(out var entity))
            {
                DealDamage(entity);
            }
        }

        private void DealDamage(Entity entity)
        {
            var damageable = entity.Get<IDamagable>();
            if (damageable != null)
            {
                damageable.TakeDamage(_damage);
                BulletOff?.Invoke(this);
            }
        }

        private void OnBecameInvisible()
        {
            BulletOff?.Invoke(this);
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}