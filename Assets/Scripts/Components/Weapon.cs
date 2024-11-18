using UnityEngine;

namespace ShootEmUp
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        private Transform _firePoint;
       
        private BulletManager _bulletManager;
        
        public void SetBulletManager(BulletManager bulletManager)
        {
            _bulletManager = bulletManager;
        }
        
        public void Attack(Vector3 direction)
        {   
            Bullet bullet = _bulletManager.SpawnBullet(_firePoint.position);
            bullet.SetVelocity(direction);
        }
        
    }
}
