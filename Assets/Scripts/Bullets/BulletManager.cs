using UnityEngine;

namespace ShootEmUp
{
    public class BulletManager : MonoBehaviour
    {
        [SerializeField]
        private Bullet _prefab;
        
        [SerializeField]
        private int _initialSizePool;
        
        private PoolObject<Bullet> _bulletPool;
        
        private void Start()
        {   
            _bulletPool = new PoolObject<Bullet>(_prefab, transform, _initialSizePool);
        }
        
        public Bullet SpawnBullet(Vector2 position)
        {
            Bullet bullet = _bulletPool.GetObject();
            bullet.SetActive(true);
            bullet.SetPosition(position);
            bullet.BulletOff += RemoveBullet;

            return bullet;
        }
        
        private void RemoveBullet(Bullet bullet)
        {
          bullet.BulletOff -= RemoveBullet;
          _bulletPool.ReturnObject(bullet);
        }
    }
}