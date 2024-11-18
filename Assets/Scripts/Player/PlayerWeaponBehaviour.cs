using UnityEngine;

namespace ShootEmUp
{
    public class PlayerWeaponBehaviour : MonoBehaviour
    {
        [SerializeField] 
        private Transform _firePoint;
        
        [SerializeField] 
        private Weapon _weapon;
        
        public void SetBulletManager(BulletManager bulletManager)
        {
            _weapon.SetBulletManager(bulletManager);
        }
        
        public void Attack()
        {
            _weapon.Attack(GetDirectionShot());
        }
        
        private Vector3 GetDirectionShot()
        {
            return _firePoint.rotation * Vector3.up;
        }
    }
}