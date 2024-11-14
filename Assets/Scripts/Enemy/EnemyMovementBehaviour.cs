using UnityEngine;

namespace ShootEmUp
{
    public class EnemyMovementBehaviour : MovementBehaviour
    {
        private Vector2 _destination;
        private bool _isPointReached;

        private void FixedUpdate()
        {
            if (CheckAbilityMove())
            {
                Move();
            }
        }

        public override void SetDirection(Vector2 direction)
        {
            _destination = direction;
            _isPointReached = false;
        }

        private bool CheckAbilityMove()
        {
            Vector2 vector = _destination - (Vector2)transform.position;
            
            if (vector.magnitude <= 0.25f)
            {
                _isPointReached = true;
                return false;
            }

            return true;
        }

        protected override void Move()
        {
            Vector2 vector = _destination - (Vector2)transform.position;
            
            Vector2 moveStep = GetMoveStep(vector);
            Vector2 nextPosition = _movement.Position + moveStep;
            _movement.MoveToPosition(nextPosition);
        }
    }
}
