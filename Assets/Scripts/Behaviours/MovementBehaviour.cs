using UnityEngine;

namespace ShootEmUp
{
    public abstract class MovementBehaviour: MonoBehaviour
    {
        [SerializeField]
        protected Movement _movement;

        [SerializeField]
        protected float _speed = 5.0f;

        protected Vector2 _currentDirection;

        public abstract void SetDirection(Vector2 direction);

        protected abstract void Move();

        protected Vector2 GetMoveStep(Vector2 direction)
        {
            return direction.normalized * Time.fixedDeltaTime * _speed;
        }
    }
}