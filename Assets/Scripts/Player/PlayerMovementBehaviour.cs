using UnityEngine;

namespace ShootEmUp
{
    public class PlayerMovementBehaviour : MonoBehaviour
    {   
        [SerializeField]
        private Movement _movement;
        
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
        
        private void Move()
        {
            Vector2 moveStep = _movement.GetMoveStep(_currentDirection);
            Vector2 nextPosition = _movement.Position + moveStep;

            if (_levelBounds.InBounds(nextPosition))
            {
                _movement.MoveToPosition(nextPosition);
            }
            else
            {
                _movement.MoveToPosition(_movement.Position);
            }
        }
        
    }
}
