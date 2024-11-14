using UnityEngine;

namespace ShootEmUp
{
    public class PlayerWeaponBehaviour : WeaponBehaviour
    {
        protected override Vector3 GetDirectionShot()
        {
            return _firePoint.rotation * Vector3.up;
        }
    }
}