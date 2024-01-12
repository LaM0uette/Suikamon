using Game.Modules.Player.Inputs;
using Obvious.Soap;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Modules.Player
{
    [RequireComponent(typeof(PlayerInputsReader))]
    public class PlayerController : MonoBehaviour
    {
        #region Statements

        private Camera _mainCamera { get; set; }
        private PlayerInputsReader _inputReader { get; set; }
        
        [Space, Title("Player Settings")]
        [SerializeField] private float _speed = 7f;
        [SerializeField] private float _maxX = 4.75f;
        
        [Space, Title("Balls")]
        [SerializeField] private FloatVariable _ballOffset;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _inputReader = GetComponent<PlayerInputsReader>();
        }

        #endregion
        
        #region Events

        private void Update()
        {
            MouseMove();
        }

        #endregion
        
        #region Functions

        private void MouseMove()
        {
            var mousePosition = Mouse.current.position.ReadValue();
            var worldPosition = _mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, _mainCamera.nearClipPlane));

            var offset = _maxX - _ballOffset;
            if (worldPosition.x > offset)
            {
                worldPosition = new Vector3(offset, worldPosition.y, worldPosition.z);
            }
            else if (worldPosition.x < -offset)
            {
                worldPosition = new Vector3(-offset, worldPosition.y, worldPosition.z);
            }
            
            var playerTransform = transform;
            var playerPosition = playerTransform.position;
            
            playerTransform.position = new Vector3(worldPosition.x, playerPosition.y, playerPosition.z);
        }

        #endregion
    }
}
