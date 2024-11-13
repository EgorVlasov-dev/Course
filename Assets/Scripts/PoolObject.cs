using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class PoolObject<T>  where T : MonoBehaviour
    {
        private readonly Queue<T> _pool = new();
        private readonly List<T> _activeObjects = new();
        private readonly T _prefab;
        private readonly Transform _container;
        private readonly int _initialSize;

        public PoolObject(T prefab, Transform container, int initialSize)
        {
            _prefab = prefab;
            _container = container;
            _initialSize = initialSize;
            InitializePool();
        }

        private void InitializePool()
        {
            for (int i = 0; i < _initialSize; i++)
            {
                T obj = Object.Instantiate(_prefab, _container);
                obj.gameObject.SetActive(false);
                _pool.Enqueue(obj);
            }
        }

        public T GetObject()
        {
            if (_pool.TryDequeue(out var obj))
            {
                obj.transform.SetParent(_container);
                obj.gameObject.SetActive(true);
                _activeObjects.Add(obj);
                return obj;
            }
            else
            {
                T newObj = Object.Instantiate(_prefab, _container);
                newObj.gameObject.SetActive(true);
                _activeObjects.Add(newObj);
                return newObj;
            }
        }

        public void ReturnObject(T obj)
        {
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
            _activeObjects.Remove(obj);
        }

        public List<T> GetActiveObjects()
        {
            return _activeObjects;
        }
    }
}
