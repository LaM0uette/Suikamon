using UnityEngine;

namespace Proto.Modules.Balls
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        #region Statements

        private Rigidbody2D _rigidbody { get; set; }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        #endregion

        #region Functions

        public void ActiveRigidbody()
        {
            _rigidbody.simulated = true;
        }

        public void Immpulse()
        {
            _rigidbody.AddForce(Vector3.down * 10f, ForceMode2D.Impulse);
        }

        #endregion
    }
}
