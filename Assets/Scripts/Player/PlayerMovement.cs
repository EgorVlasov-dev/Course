using UnityEngine;

namespace ShootEmUp
{
    public class PlayerMovement : MonoBehaviour, IMovable
    {
        [SerializeField] 
        private float _speed = 5.0f;
        [SerializeField] 
        private Rigidbody2D _rigidbody;

        [Space] 
        [SerializeField] 
        private LevelBounds _levelBounds;

        private Vector2 _currentDirection;

        public void SetDirection(Vector2 direction)
        {
            _currentDirection = direction;
        }

        private void FixedUpdate()
        {
            Move();
        }

        public void Move()
        {
            Vector2 moveStep = _currentDirection * Time.fixedDeltaTime * _speed;
            Vector2 targetPosition = _rigidbody.position + moveStep;
            
            if (_levelBounds.InBounds(targetPosition))
            {
                _rigidbody.MovePosition(targetPosition);
            }
            else
            {
                _rigidbody.MovePosition(_rigidbody.position);
            }
        }
    }
}
