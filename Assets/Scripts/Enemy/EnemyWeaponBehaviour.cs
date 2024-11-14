using UnityEngine;

namespace ShootEmUp
{
    public class EnemyWeaponBehaviour : WeaponBehaviour
    {
        [SerializeField] 
        private Weapon _weapon;
        
        [SerializeField] 
        private Transform _firePoint;
        
        [Space] 
        [SerializeField] 
        private float _countdown;
        
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

        protected override Vector3 GetDirectionShot()
        {
            Vector3 startPosition = _firePoint.position;

            Vector3 vector = _target.position - startPosition;
            Vector3 direction = vector.normalized;
            
            return direction;
        }

        public override void Attack()
        {
            _weapon.Attack(GetDirectionShot());
        }
    }
}