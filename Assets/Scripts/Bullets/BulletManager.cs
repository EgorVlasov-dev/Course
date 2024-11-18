using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletManager : MonoBehaviour
    {
        [SerializeField]
        private Bullet _prefab;
        
        [SerializeField]
        private int _initialSizePool;

        [SerializeField] 
        private LevelBounds _levelBounds;
        
        private PoolObject<Bullet> _bulletPool;
        
        private void Start()
        {   
            _bulletPool = new PoolObject<Bullet>(_prefab, transform, _initialSizePool);
        }

        private void Update()
        {
            CheckingExitBulletsBeyondLevel();
        }

        private void CheckingExitBulletsBeyondLevel()
        {
            var activeBullet = _bulletPool.GetActiveObjects();

            for (int i = 0; i < activeBullet.Count; i++)
            {
                if (_levelBounds.InBounds(activeBullet[i].Position) == false)
                {
                    activeBullet[i].Disable();
                }
            }
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