
using System;

public interface IDamagable
{
    public event Action<int> OnHealthChanged;
    public event Action OnHealthEmpty;

    public void TakeDamage(int damage);
}
