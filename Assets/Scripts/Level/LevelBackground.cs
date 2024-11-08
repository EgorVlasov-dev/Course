using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour
    {   
        [SerializeField] private float _movingSpeedY;
        [Space]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private Vector3 _startPosition;
        private float _movementDistance;

        private float _currentDistanceTraveled;
        
        private void Start()
        {
            _startPosition = transform.position;
            _movementDistance = _spriteRenderer.bounds.size.y;
        }
        
        private void Update()
        {
            if (CheckIfTraveledFullDistance())
            {
                MoveToStartPosition();
            }
            
            MoveDown();
        }
        
        private bool CheckIfTraveledFullDistance()
        {
            return _currentDistanceTraveled >= _movementDistance;
        }
        
        private void MoveToStartPosition()
        {
            _currentDistanceTraveled = 0;
            transform.position = _startPosition;
        }

        private void MoveDown()
        {
            var offsetY = _movingSpeedY * Time.deltaTime;
            transform.position -= new Vector3(transform.position.x, offsetY, transform.position.z);
            
            _currentDistanceTraveled += offsetY;
        }
    }
}