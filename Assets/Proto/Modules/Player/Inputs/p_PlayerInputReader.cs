using UnityEngine;
using UnityEngine.InputSystem;

namespace Proto.Modules.Player.Inputs
{
    public class p_PlayerInputReader : MonoBehaviour
    {
        #region Statements

        public float MovementValue { get; private set; }

        #endregion

        #region Events

        public void OnMove(InputValue value)
        {
            var movementInput = value.Get<Vector2>().x;
            MovementValue = movementInput < 0 ? -1 : movementInput > 0 ? 1 : 0;
        }
        
        #endregion
    }
}
