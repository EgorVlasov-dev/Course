using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShootEmUp
{
    public class PlayerInputSystem : MonoBehaviour
    {
        public event Action OnFired;
        public event Action<Vector2> Moved;

        private InputConfigs _inputConfigs;

        private void Awake()
        {
            _inputConfigs = new();
            _inputConfigs.Enable();
        }

        private void OnEnable()
        {
            _inputConfigs.Gameplay.Fire.performed += OnFirePerformed;
        }

        private void OnDisable()
        {
            _inputConfigs.Gameplay.Fire.performed -= OnFirePerformed;
        }

        private void OnFirePerformed(InputAction.CallbackContext obj)
        {
            OnFired?.Invoke();
        }

        private void Update()
        {
            ReadMovement();
        }

        private void ReadMovement()
        {
            var direction = _inputConfigs.Gameplay.Movement.ReadValue<Vector2>();
            Moved?.Invoke(direction);
        }
    }
}
