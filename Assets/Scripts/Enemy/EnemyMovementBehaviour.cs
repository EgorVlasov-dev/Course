using UnityEngine;

namespace ShootEmUp
{
    public class EnemyMovementBehaviour : MonoBehaviour
    {   
        [SerializeField]
        protected Movement _movement;
        
        private Vector2 _destination;
        private bool _isPointReached;

        private void FixedUpdate()
        {
            if (CheckAbilityMove())
            {
                Move();
            }
        }

        public void SetDestination(Vector2 destination )
        {
            _destination = destination ;
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

        private void Move()
        {
            Vector2 vector = _destination - (Vector2)transform.position;
            
            Vector2 moveStep = _movement.GetMoveStep(vector);
            Vector2 nextPosition = _movement.Position + moveStep;
            _movement.MoveToPosition(nextPosition);
        }
    }
}
