using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class PlayerController : MonoBehaviour
    {
        [FormerlySerializedAs("_player")] [SerializeField] private Entity entity;
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
            entity.GetComponentImplementing<IControllable>().SetDirection(direction);
        }
        
        private void OnPlayerFired()
        {
            entity.GetComponentImplementing<IAttacker>().Attack();
        }
    }
}