using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Modules.Balls
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        #region Statements
        
        private Rigidbody2D _rigidbody { get; set; }
        
        [Space, Title("Balls")]
        public int BallIndex;
        
        private bool _isCollided { get; set; }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        #endregion
    }
}
