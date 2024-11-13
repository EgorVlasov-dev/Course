using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovable
{   
    [SerializeField]
    public float _speed = 5.0f;
    [Space]
    [SerializeField]
    public Rigidbody2D _rigidbody;

    private Vector2 _currentDirection;

    public void SetDirection(Vector2 direction)
    {
        _currentDirection = direction;
    }
    
    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        Vector2 moveStep = _currentDirection * Time.fixedDeltaTime * _speed;
        Vector2 targetPosition = _rigidbody.position + moveStep;
        _rigidbody.MovePosition(targetPosition);
    }
}
