using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _spawnPositions;

        [SerializeField]
        private Transform[] _attackPositions;
        
        [SerializeField]
        private Entity _character;
        
        [SerializeField]
        private Transform _container;
        
        [SerializeField]
        private int _enemyPoolInitSize;
        
        [SerializeField]
        private Entity _prefab;

        [SerializeField] 
        private BulletManager _bulletManager;
        
        private PoolObject<Entity> _enemyPool;
        
        private Queue<Entity> _waitingToShoot = new();
        private HashSet<Entity> _activeShootingEnemies = new();
        
        private void Awake()
        {   
            _enemyPool = new PoolObject<Entity>(_prefab, _container, _enemyPoolInitSize);
        }
        
        public void SpawnEnemy()
        {
            Entity enemy = _enemyPool.GetObject();
            SetupEnemy(enemy);

            if (_enemyPool.GetActiveObjects().Count < 5)
            {   
                StartShooting(enemy);
            }
            else
            {
                _waitingToShoot.Enqueue(enemy);
            }
        }

        private void SetupEnemy(Entity enemy)
        {
            enemy.Get<IDamagable>().OnHealthEmpty += EnemyDead;
            Transform spawnPosition = RandomPoint(_spawnPositions);
            enemy.Transform.position = spawnPosition.position;

            Transform attackPosition = RandomPoint(_attackPositions);
            enemy.Get<EnemyMovementBehaviour>().SetDestination(attackPosition.position);
            enemy.Get<EnemyWeaponBehaviour>().SetBulletManager(_bulletManager);
            enemy.Get<EnemyWeaponBehaviour>().SetTargetTransform(_character.Transform);
        }
        
        private Transform RandomPoint(Transform[] points)
        {
            int index = Random.Range(0, points.Length);
            return points[index];
        }
        
        private void StartShooting(Entity enemy)
        {
            _activeShootingEnemies.Add(enemy);
        }
        
        private void EnemyDead(Entity enemy)
        {
           _enemyPool.ReturnObject(enemy);
            
            enemy.Get<IDamagable>().OnHealthEmpty -= EnemyDead;
            
            _activeShootingEnemies.Remove(enemy);
            
            if (_waitingToShoot.Count > 0)
            {
                Entity nextEnemy = _waitingToShoot.Dequeue();
                StartShooting(nextEnemy);
            }
        }
    }
}