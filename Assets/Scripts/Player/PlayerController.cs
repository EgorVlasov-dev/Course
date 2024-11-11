using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private BulletManager _bulletManager;
        
        [SerializeField] private PlayerInputSystem _inputSystem;

        private void OnEnable()
        {
            _inputSystem.Moved += OnPlayerMoved;
            _inputSystem.OnFired += OnPlayerFired;
        }

        private void OnDisable()
        {
            _inputSystem.Moved += OnPlayerMoved;
            _inputSystem.OnFired += OnPlayerFired;
        }

        private void OnPlayerMoved(Vector2 direction)
        {
            _player.GetComponentImplementing<IControllable>().SetDirection(direction);
        }
        
        private void OnPlayerFired()
        {
            _player.GetComponentImplementing<IAttacker>().Attack();
        }
    }
}