using System;
using UnityEngine;

namespace ShootEmUp
{
    public class PlayerInput : MonoBehaviour
    {
        public event Action OnFired;
        public event Action<Vector2> Moved;
        
        private void Update()
        {
            Movement();
            Fire();
        }

        private void Movement()
        {   
            float moveX = Input.GetAxis("Horizontal");
            Vector2 direction = new Vector2(moveX, 0);
            
            Moved?.Invoke(direction);
        }
        
        private void Fire()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnFired?.Invoke();
            }
        }
    }
}
