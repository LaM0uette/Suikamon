using Game.Modules.Manager;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Modules.Balls
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        #region Statements
        
        private Rigidbody2D _rigidbody { get; set; }
        private bool _isCollided { get; set; }
        
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
            if (!other.transform.TryGetComponent(out Ball ball))
                return;
            
            _isCollided = true;

            if (ball.BallIndex == BallIndex && BallIndex < GameManager.Instance.Balls.Length - 1)
            {
                var nextBall = GameManager.Instance.Balls[++BallIndex];
                var ballGo = Instantiate(nextBall, transform.position, Quaternion.identity, GameManager.Instance.BallsParent.transform);
                var newBall = ballGo.GetComponent<Ball>();
                newBall.ActiveRigidbody();
                newBall.Immpulse();
                
                Destroy(ball.gameObject);
                Destroy(gameObject);
            }
        }
        
        private void OnCollisionExit2D(Collision2D _)
        {
            _isCollided = false;
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("DeadZone")) return;
            
            if (_isCollided)
            {
                GameManager.Instance.GameOver();
                
                var balls = GameManager.Instance.BallsParent.GetComponentsInChildren<Ball>();
                foreach (var ball in balls)
                {
                    ball.DeactiveRigidbody();
                }
            }
        }

        #endregion

        #region Functions

        public void ActiveRigidbody()
        {
            _rigidbody.simulated = true;
        }
        
        public void DeactiveRigidbody()
        {
            _rigidbody.simulated = false;
        }

        public void Immpulse()
        {
            _rigidbody.AddForce(Vector3.down * 10f, ForceMode2D.Impulse);
        }

        #endregion
    }
}
