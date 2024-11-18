using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {   
        public event Action<Bullet> BulletOff;

        public Vector3 Position => transform.position;
        
        [SerializeField]
        private int _damage;

        [SerializeField]
        private Rigidbody2D _rigidbody2D;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField] 
        private float MagnitudeVelocity = 3;
        
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
        
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetVelocity(Vector2 velocity)
        {
            _rigidbody2D.velocity = velocity.normalized * MagnitudeVelocity;
        }

        public void Disable()
        {   
            gameObject.SetActive(false);
            BulletOff?.Invoke(this);
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
    }
}