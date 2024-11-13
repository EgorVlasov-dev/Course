using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] 
        private Entity _entity;
        [SerializeField] 
        private BulletManager _bulletManager;
        
        [SerializeField] 
        private PlayerInputSystem _inputSystem;

        private void OnEnable()
        {
            _inputSystem.Moved += OnPlayerMoved;
            _inputSystem.OnFired += OnPlayerFired;
            _entity.GetComponentImplementing<IDamagable>().OnHealthEmpty += PlayerDead;
        }
        
        private void OnDisable()
        {
            _inputSystem.Moved += OnPlayerMoved;
            _inputSystem.OnFired += OnPlayerFired;
            _entity.GetComponentImplementing<IDamagable>().OnHealthEmpty -= PlayerDead;
        }

        private void OnPlayerMoved(Vector2 direction)
        {
            _entity.GetComponentImplementing<IMovable>().SetDirection(direction);
        }
        
        private void OnPlayerFired()
        {
            _entity.GetComponentImplementing<IAttacker>().Attack();
        }
        
        private void PlayerDead(Entity player)
        {   
            player.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
    }
}