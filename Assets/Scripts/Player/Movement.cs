using UnityEngine;

namespace ShootEmUp
{
    public class Movement : MonoBehaviour
    {
        public Vector2 Position => _rigidbody.position;
        
        [SerializeField] 
        private Rigidbody2D _rigidbody;
        
        [SerializeField]
        protected float _speed = 5.0f;
        
        public void MoveToPosition(Vector2 position)
        {
            _rigidbody.MovePosition(position);
        }
        
        public Vector2 GetMoveStep(Vector2 direction)
        {
            return direction.normalized * Time.fixedDeltaTime * _speed;
        }
    }
}