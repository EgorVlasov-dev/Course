using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] 
        private Entity _entity;

        [Space]
        [SerializeField] 
        private PlayerInput input;

        private void OnEnable()
        {
            input.Moved += OnPlayerMoved;
            input.OnFired += OnPlayerFired;
        }
        
        private void OnDisable()
        {
            input.Moved += OnPlayerMoved;
            input.OnFired += OnPlayerFired;
        }

        private void OnPlayerMoved(Vector2 direction)
        {
            _entity.Get<PlayerMovementBehaviour>().SetDirection(direction);
        }
        
        private void OnPlayerFired()
        { 
            _entity.Get<WeaponBehaviour>().Attack();
        }
    }
}