using System.Collections;
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
        private Transform worldTransform;

        [SerializeField]
        private Transform _container;
        
        [SerializeField]
        private int enemyPoolInitSize;
        
        [SerializeField]
        private Entity _prefab;
        
        [SerializeField]
        private BulletManager _bulletSystem;
        
        private readonly HashSet<Entity> _activeEnemies = new();
        private readonly Queue<Entity> _enemyPool = new();
        
        private void Awake()
        {
            InitEnemy();
        }

        private void InitEnemy()
        {
            for (var i = 0; i < enemyPoolInitSize; i++)
            {
                 Entity enemy = Instantiate(_prefab, _container);
                 enemy.gameObject.SetActive(false);
                 _enemyPool.Enqueue(enemy);
            }
        }

        private void Start()
        {
            StartCoroutine(SpawnEnemiesCoroutine());
        }

        private IEnumerator SpawnEnemiesCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(1, 2));
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            Entity enemy = GetEnemyFromPool();
            enemy.gameObject.SetActive(true);
            SetupEnemy(enemy);

            if (_activeEnemies.Count < 5 && _activeEnemies.Add(enemy))
            {
                enemy.GetComponentImplementing<EnemyAttack>().OnFire += OnFire;
            }
        }

        private Entity GetEnemyFromPool()
        {
            if (!_enemyPool.TryDequeue(out Entity enemy))
            {
                enemy = Instantiate(_prefab, _container);
            }
            return enemy;
        }

        private void SetupEnemy(Entity enemy)
        {
            enemy.GetComponentImplementing<IDamagable>().OnHealthEmpty += EnemyDead;
            Transform spawnPosition = RandomPoint(_spawnPositions);
            enemy.transform.position = spawnPosition.position;

            Transform attackPosition = RandomPoint(_attackPositions);
            enemy.GetComponentImplementing<IMovable>().SetDirection(attackPosition.position);
            enemy.GetComponentImplementing<EnemyAttack>().SetTargetTransform(_character.transform);
        }
        
        private Transform RandomPoint(Transform[] points)
        {
            int index = Random.Range(0, points.Length);
            return points[index];
        }
        
        private void OnFire(Vector2 position, Vector2 direction)
        {
            Bullet bullet = _bulletSystem.SpawnBullet(position);
            bullet.SetVelocity(direction);
        }

        private void EnemyDead(Entity enemy)
        {
            _activeEnemies.Remove(enemy);
            _enemyPool.Enqueue(enemy);
            
            enemy.transform.SetParent(_container);
            enemy.gameObject.SetActive(false);
            
            enemy.GetComponentImplementing<IDamagable>().OnHealthEmpty -= EnemyDead;
            enemy.GetComponentImplementing<EnemyAttack>().OnFire -= OnFire;
        }
    }
}