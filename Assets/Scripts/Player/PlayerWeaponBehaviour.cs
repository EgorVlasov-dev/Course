using UnityEngine;

namespace ShootEmUp
{
    public class PlayerWeaponBehaviour : WeaponBehaviour
    {
        [SerializeField] 
        private Transform _firePoint;
        
        [SerializeField] 
        private Weapon _weapon;
        
        protected override Vector3 GetDirectionShot()
        {
            return _firePoint.rotation * Vector3.up;
        }

        public override void Attack()
        {
            _weapon.Attack(GetDirectionShot());
        }
    }
}