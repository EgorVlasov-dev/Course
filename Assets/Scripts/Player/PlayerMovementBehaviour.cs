using UnityEngine;

namespace ShootEmUp
{
    public class PlayerMovementBehaviour : MovementBehaviour
    {
        [Space] 
        [SerializeField] 
        private LevelBounds _levelBounds;

        public override void SetDirection(Vector2 direction)
        {
            _currentDirection = direction;
        }

        private void FixedUpdate()
        {
            Move();
        }
        
        protected override void Move()
        {
            Vector2 moveStep = GetMoveStep(_currentDirection);
            Vector2 targetPosition = _movement.Position + moveStep;

            if (_levelBounds.InBounds(targetPosition))
            {
                _movement.MoveToPosition(targetPosition);
            }
            else
            {
                _movement.MoveToPosition(_movement.Position);
            }
        }
    }
}
