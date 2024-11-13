using UnityEngine;

namespace ShootEmUp
{
    public abstract class BulletManager : MonoBehaviour
    {
        [SerializeField]
        private Bullet _prefab;

        [SerializeField]
        private LevelBounds _levelBounds;
        
        [SerializeField]
        private int _initialSizePool;
        
        private PoolObject<Bullet> _bulletPool;
        
        private void Start()
        {   
            _bulletPool = new PoolObject<Bullet>(_prefab, transform, _initialSizePool);
        }

        private void Update()
        {
            CheckingBulletsOutside();
        }
        
        public Bullet SpawnBullet(Vector2 position)
        {
            Bullet bullet = _bulletPool.GetObject();
            bullet.gameObject.SetActive(true);
            bullet.SetPosition(position);
            bullet.BulletOff += OnBulletCollision;

            return bullet;
        }
        
        private void CheckingBulletsOutside()
        {
            for (int i = 0; i < _bulletPool.GetActiveObjects().Count; i++)
            {
                if (_levelBounds.InBounds(_bulletPool.GetActiveObjects()[i].transform.position) == false)
                {
                    RemoveBullet(_bulletPool.GetActiveObjects()[i]);
                }
            }
        }
        
        private void OnBulletCollision(Bullet bullet)
        {
            RemoveBullet(bullet);
        }
        
        private void RemoveBullet(Bullet bullet)
        {
          bullet.BulletOff -= OnBulletCollision;
          _bulletPool.ReturnObject(bullet);
        }
    }
}