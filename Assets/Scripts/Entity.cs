using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Entity : MonoBehaviour
    {
        public Transform Transform => transform;
        
        [SerializeField] 
        private List<MonoBehaviour> _components;
   
        public T Get<T>() where T : class
        {
            foreach (var component in _components)
            {
                if (component is T)
                {
                    return component as T;
                }
            }
            return null;
        }
    
        public void SetActive(bool IsActive)
        {
            gameObject.SetActive(IsActive);
        }
        
    }
}