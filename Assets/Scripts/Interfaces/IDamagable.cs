using System;

namespace ShootEmUp
{
    public interface IDamagable
    {
        public event Action<int> OnHealthChanged;
        public event Action<Entity> OnHealthEmpty;

        public void TakeDamage(int damage);
    }
}
