using System;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyAttack : MonoBehaviour, IAttacker
    {
        public event Action<Vector2, Vector2> OnFire;

        [SerializeField] 
        private Transform _firePoint;
        
        [Space] 
        [SerializeField] private float _countdown;

        private Transform _target;
        private float _currentTime;

        private void Update()
        {
            if (CheckCanAttack())
            {
                Attack();
            }
        }

        public void SetTargetTransform(Transform target)
        {
            _target = target;
        }

        private bool CheckCanAttack()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= _countdown && _target != null)
            {
                _currentTime = 0;
                return true;
            }

            return false;
        }

        public void Attack()
        {
            Vector2 startPosition = _firePoint.position;

            Vector2 vector = (Vector2)_target.position - startPosition;
            Vector2 direction = vector.normalized;

            OnFire?.Invoke(startPosition, direction);
        }
    }
}