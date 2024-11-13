using UnityEngine;

namespace ShootEmUp
{
    public class EnemyMovement : MonoBehaviour, IMovable
    {
        [SerializeField] public float _speed = 5.0f;
        [Space] [SerializeField] private Rigidbody2D _rigidbody;

        private Vector2 _destination;
        private bool _isPointReached;

        private void FixedUpdate()
        {
            Move();
        }

        public void SetDirection(Vector2 direction)
        {
            _destination = direction;
            _isPointReached = false;
        }

        public void Move()
        {
            Vector2 vector = _destination - (Vector2)transform.position;
            if (vector.magnitude <= 0.25f)
            {
                _isPointReached = true;
                return;
            }

            Vector2 direction = vector.normalized * Time.fixedDeltaTime;
            Vector2 nextPosition = _rigidbody.position + direction * _speed;
            _rigidbody.MovePosition(nextPosition);
        }
    }
}
