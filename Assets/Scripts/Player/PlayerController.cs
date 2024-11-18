using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] 
        private Entity _entity;
        
        [Space]
        [SerializeField] 
        private PlayerInput _input;

        [Space] 
        [SerializeField] 
        private BulletManager _bulletManager;

        private void Start()
        {
            _entity.Get<PlayerWeaponBehaviour>().SetBulletManager(_bulletManager);
        }

        private void OnEnable()
        {
            _input.Moved += OnPlayerMoved;
            _input.OnFired += OnPlayerFired;
        }
        
        private void OnDisable()
        {
            _input.Moved += OnPlayerMoved;
            _input.OnFired += OnPlayerFired;
        }

        private void OnPlayerMoved(Vector2 direction)
        {
            _entity.Get<PlayerMovementBehaviour>().SetDirection(direction);
        }
        
        private void OnPlayerFired()
        { 
            _entity.Get<PlayerWeaponBehaviour>().Attack();
        }
    }
}