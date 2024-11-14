using UnityEngine;

namespace ShootEmUp
{
    public class Movement : MonoBehaviour
    {
        public Vector2 Position => _rigidbody.position;
        
        [SerializeField] 
        private Rigidbody2D _rigidbody;
        
        public void MoveToPosition(Vector2 position)
        {
            _rigidbody.MovePosition(position);
        }
    }
}