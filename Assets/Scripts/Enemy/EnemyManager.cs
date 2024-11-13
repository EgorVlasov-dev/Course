using System.Collections;
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
        
        private PoolObject<Entity> _enemyPool;
        
        private void Awake()
        {   
            _enemyPool = new PoolObject<Entity>(_prefab, _container, enemyPoolInitSize);
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
            Entity enemy = _enemyPool.GetObject();
            enemy.gameObject.SetActive(true);
            SetupEnemy(enemy);

            if (_enemyPool.GetActiveObjects().Count < 5)
            {
                enemy.GetComponentImplementing<EnemyAttack>().OnFire += OnFire;
            }
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
           _enemyPool.ReturnObject(enemy);
            
            enemy.GetComponentImplementing<IDamagable>().OnHealthEmpty -= EnemyDead;
            enemy.GetComponentImplementing<EnemyAttack>().OnFire -= OnFire;
        }
    }
}