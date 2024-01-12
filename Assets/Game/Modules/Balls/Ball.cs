using Game.Modules.Manager;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Modules.Balls
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        #region Statements
        
        private Rigidbody2D _rigidbody { get; set; }
        private bool _isCollided { get; set; }
        private float _deadDeltaTime { get; set; }
        
        [Space, Title("Balls")]
        [SerializeField] private int _ballIndex;
        
        [Space, Title("Time")]
        [SerializeField] private float _deadTime = 2f;
        
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

            if (ball._ballIndex == _ballIndex && _ballIndex < GameManager.Instance.Balls.Length - 1)
            {
                var nextBall = GameManager.Instance.Balls[++_ballIndex];
                var ballGo = Instantiate(nextBall, other.transform.position, Quaternion.identity, GameManager.Instance.BallsParent.transform);
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
            
            _deadDeltaTime += Time.deltaTime;
            if (_deadDeltaTime > _deadTime && _isCollided)
            {
                GameManager.Instance.GameOver();
                    
                var balls = GameManager.Instance.BallsParent.GetComponentsInChildren<Ball>();
                foreach (var ball in balls)
                {
                    ball.DeactiveRigidbody();
                }
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("DeadZone")) return;
            
            _deadDeltaTime = 0f;
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
            _rigidbody.AddForce(Vector3.down * 30f, ForceMode2D.Impulse);
        }

        #endregion
    }
}
