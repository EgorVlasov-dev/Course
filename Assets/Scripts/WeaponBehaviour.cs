using UnityEngine;

namespace ShootEmUp
{
    public abstract class WeaponBehaviour : MonoBehaviour
    {   
        public abstract void Attack();
        protected abstract Vector3 GetDirectionShot();
    }
}
