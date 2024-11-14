using UnityEngine;

namespace ShootEmUp
{
    public abstract class WeaponBehaviour : MonoBehaviour
    {   
        [SerializeField]
        protected Weapon _weapon;

        [SerializeField]
        protected Transform _firePoint;

        protected abstract Vector3 GetDirectionShot();

        public void Attack()
        {
            _weapon.Attack(GetDirectionShot());
        }
    }
}
