using Proto.Modules.Manager;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Proto.Modules.Balls
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class p_Ball : MonoBehaviour
    {
        #region Statements
        
        private Rigidbody2D _rigidbody { get; set; }
        
        [Space, Title("Balls")]
        public int BallIndex;
        
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

            if (ball.BallIndex == BallIndex)
            {
                var nextBall = p_GameManager.Instance.Balls[++BallIndex];
                var ballGo = Instantiate(nextBall, transform.position, Quaternion.identity, p_GameManager.Instance.BallsParent.transform);
                var newBall = ballGo.GetComponent<p_Ball>();
                newBall.ActiveRigidbody();
                newBall.Immpulse();
                
                Destroy(ball.gameObject);
                Destroy(gameObject);
            }
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
