using UnityEngine;

namespace ShootEmUp
{
    public class PlayerAttack : MonoBehaviour, IAttacker
    {
        [SerializeField]
        public Transform firePoint;
        
        public void Attack()
        {
            // _bulletManager.SpawnBullet(
            //     this._player.firePoint.position,
            //     Color.blue,
            //     (int) PhysicsLayer.PLAYER_BULLET,
            //     1,
            //     true,
            //     this._player.firePoint.rotation * Vector3.up * 3
            // );

            Debug.Log("Blup");
        }
    }
}
