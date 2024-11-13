using UnityEngine;

namespace ShootEmUp
{
    public interface IMovable
    {
        public void SetDirection(Vector2 direction);

        public void Move();
    }
}
