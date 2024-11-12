using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletManager : MonoBehaviour
    {
        [SerializeField]
        private Bullet _prefab;

        [SerializeField]
        private LevelBounds _levelBounds;
        
        [SerializeField]
        private Transform _spawnTransform;
        
        [SerializeField]
        private int _initialSizePool;
        
        private readonly Queue<Bullet> _bulletPool = new();
        private readonly List<Bullet> _activeBullets = new();
        
        private void Start()
        {
            InitCreateBullets();
        }

        private void Update()
        {
            CheckingBulletsOutside();
        }

        private void InitCreateBullets()
        {
            for (int i = 0; i < _initialSizePool; i++)
            {
                Bullet bullet = Instantiate(_prefab, transform);
                bullet.gameObject.SetActive(false);
                _bulletPool.Enqueue(bullet);
            }
        }
        
        public Bullet SpawnBullet()
        {
            Bullet bullet = GetBullet();
            bullet.gameObject.SetActive(true);
            bullet.SetPosition(_spawnTransform.position);
            bullet.BulletOff += OnBulletCollision;
            _activeBullets.Add(bullet);

            return bullet;
        }
        
        private Bullet GetBullet()
        {   
           if (_bulletPool.TryDequeue(out var bullet))
           {
               bullet.transform.SetParent(transform);
               return bullet;
           }
           else
           {
               bullet = Instantiate(_prefab, transform);
               bullet.BulletOff += OnBulletCollision;
               return bullet;
           }
        }
        
        private void CheckingBulletsOutside()
        {
            for (int i = 0; i < _activeBullets.Count; i++)
            {
                if (_levelBounds.InBounds(_activeBullets[i].transform.position) == false)
                {
                    RemoveBullet(_activeBullets[i]);
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
          _bulletPool.Enqueue(bullet);
          _activeBullets.Remove(bullet);
          bullet.gameObject.SetActive(false);
        }
    }
}