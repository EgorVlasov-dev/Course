using System.Collections;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] 
        private float _minTimeBetweenSpawns = 1;

        [SerializeField] 
        private float _maxTimeBetweenSpawns = 2;
        
        [SerializeField] 
        private EnemyManager _enemyManager;
        
        private void Start()
        {
            StartCoroutine(SpawnEnemiesCoroutine());
        }

        private IEnumerator SpawnEnemiesCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(_minTimeBetweenSpawns, _maxTimeBetweenSpawns));
                _enemyManager.SpawnEnemy();
            }
        }
    }
}
