using System;
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
        
        private bool _isCollided { get; set; }
        
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
            
            _isCollided = true;

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
        
        private void OnCollisionExit2D(Collision2D other)
        {
            _isCollided = false;
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("DeadZone")) return;
            
            if (_isCollided)
            {
                p_GameManager.Instance.GameOver();
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
