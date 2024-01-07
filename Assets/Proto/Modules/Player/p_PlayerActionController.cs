using System.Collections;
using Proto.Modules.Balls;
using Proto.Modules.Manager;
using Proto.Modules.Player.Inputs;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Proto.Modules.Player
{
    [RequireComponent(typeof(p_PlayerInputReader))]
    public class p_PlayerActionController : MonoBehaviour
    {
        #region Statements

        private p_PlayerInputReader _inputReader { get; set; }
        
        [Space, Title("Balls")]
        [SerializeField] private GameObject _playerBallParent;
        [SerializeField] private GameObject _nextBallParent;

        private GameObject _nextBall;
        private GameObject _nextNextBall;
        private GameObject _currentBall;
        
        private bool _canDropBall = true;
        private float _cooldown = 0.4f;
        
        private void Awake()
        {
            _inputReader = GetComponent<p_PlayerInputReader>();
        }

        private void Start()
        {
            _nextNextBall = GetNextBall();
            SpawnNextBall();
        }

        #endregion

        #region Events

        private void OnEnable()
        {
            _inputReader.DropBallEvent += DropBall;
        }

        private void OnDisable()
        {
            _inputReader.DropBallEvent -= DropBall;
        }

        #endregion

        #region Functions
        
        private void SpawnNextBall()
        {
            _nextBall = _nextNextBall;
            
            var ballSize = _nextBall.transform.localScale;
            p_GameManager.BallOffset = ballSize.x / 2;
            
            _currentBall = Instantiate(_nextBall, _playerBallParent.transform.position, Quaternion.identity, _playerBallParent.transform);
            
            _nextNextBall = GetNextBall();

            for (var i = 0; i < _nextBallParent.transform.childCount; i++)
                Destroy(_nextBallParent.transform.GetChild(i).gameObject);
            
            Instantiate(_nextNextBall, _nextBallParent.transform.position, Quaternion.identity, _nextBallParent.transform);
        }
        
        private static GameObject GetNextBall()
        {
            var balls = p_GameManager.Instance.Balls;
            return balls[Random.Range(0, 5)];
        }

        private void DropBall()
        {
            if (p_GameManager.IsGameOver || !_canDropBall) 
                return;
            
            _canDropBall = false;
            _currentBall.transform.SetParent(p_GameManager.Instance.BallsParent.transform);
            
            var ball = _currentBall.GetComponent<p_Ball>();
            ball.ActiveRigidbody();
            ball.Immpulse();
            
            SpawnNextBall();
            StartCoroutine(Cooldown());
        }
        
        private IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(_cooldown);
            _canDropBall = true;
        }

        #endregion
    }
}
