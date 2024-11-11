using System;
using UnityEngine;

namespace ShootEmUp
{
    public class Health : MonoBehaviour, IDamagable
    {
        public event Action<int> OnHealthChanged;
        public event Action OnHealthEmpty;

        [SerializeField] private int _maxHealth;

        private int _currentHealth;

        private void Start()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                return;
            }

            _currentHealth -= damage;
            OnHealthChanged?.Invoke(_currentHealth);

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            OnHealthEmpty?.Invoke();
            Time.timeScale = 0;
        }
    }
}
