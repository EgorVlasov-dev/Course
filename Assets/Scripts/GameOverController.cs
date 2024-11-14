using UnityEngine;

namespace ShootEmUp
{
    public class GameOverController : MonoBehaviour
    {
        [SerializeField] private Entity _player;
        
        private void OnEnable()
        {
            _player.Get<IDamagable>().OnHealthEmpty += PlayerDead;
        }
        
        private void OnDisable()
        {
            _player.Get<IDamagable>().OnHealthEmpty -= PlayerDead;
        }
        
        private void PlayerDead(Entity player)
        {
            player.SetActive(false);
            StopGame();
        }

        private void StopGame()
        {
            Time.timeScale = 0;
        }
    }
}
