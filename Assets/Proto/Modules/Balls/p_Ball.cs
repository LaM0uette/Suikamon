using UnityEngine;

namespace Proto.Modules.Balls
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class p_Ball : MonoBehaviour
    {
        #region Statements

        private Rigidbody2D _rigidbody { get; set; }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        #endregion

        #region Events

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.transform.TryGetComponent(out p_Ball ball))
                return;
            
            Debug.Log(ball.name);
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
