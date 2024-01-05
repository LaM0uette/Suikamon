using JetBrains.Annotations;
using Proto.Modules.Player.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Proto.Modules.Player
{
    [RequireComponent(typeof(p_PlayerInputReader))]
    public class p_PlayerController : MonoBehaviour
    {
        #region Statements

        [NotNull] private Camera _mainCamera { get; set; }
        private p_PlayerInputReader _inputReader { get; set; }
        
        [SerializeField] private float _speed = 7f;
        [SerializeField] private float _minX = -10f;
        [SerializeField] private float _maxX = 10f;

        private void Awake()
        {
            _mainCamera = Camera.main!;
            _inputReader = GetComponent<p_PlayerInputReader>();
        }

        #endregion

        #region Events

        private void Update()
        {
            // Move();
            MouseMove();
        }

        #endregion

        #region Functions

        private void Move()
        {
            var movement = new Vector3(_inputReader.MovementValue, 0f, 0);
            transform.position += movement * (_speed * Time.deltaTime);
        }

        private void MouseMove()
        {
            var mousePosition = Mouse.current.position.ReadValue();
            var worldPosition = _mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, _mainCamera.nearClipPlane));

            var playerTransform = transform;
            var playerPosition = playerTransform.position;
            
            playerTransform.position = new Vector3(worldPosition.x, playerPosition.y, playerPosition.z);
        }

        #endregion
    }
}
