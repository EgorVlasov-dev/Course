using UnityEngine;

namespace ShootEmUp
{
    public class PlayerAttack : MonoBehaviour, IAttacker
    {
        [SerializeField]
        public Transform _firePoint;
        
        [SerializeField] 
        private BulletManager _bulletManager;
        
        public void Attack()
        {
            Bullet bullet = _bulletManager.SpawnBullet(_firePoint.position);
            bullet.SetVelocity(_firePoint.rotation * Vector3.up);
        }
    }
}
