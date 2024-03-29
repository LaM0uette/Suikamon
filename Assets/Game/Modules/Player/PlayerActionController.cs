using System.Collections;
using Game.Modules.Balls;
using Game.Modules.Manager;
using Game.Modules.Player.Inputs;
using Obvious.Soap;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Modules.Player
{
    [RequireComponent(typeof(PlayerInputsReader))]
    public class PlayerActionController : MonoBehaviour
    {
        #region Statements

        private PlayerInputsReader _inputReader { get; set; }
        
        [Space, Title("Balls")]
        [SerializeField] private GameObject _playerBallParent;
        [SerializeField] private GameObject _nextBallParent;
        [SerializeField] private FloatVariable _ballOffset;
        
        [Space, Title("Settings")]
        [SerializeField] private int _minBallId;
        [SerializeField] private int _maxBallId = 4;
        [SerializeField] private float _cooldown = 0.4f;
        
        private int _nextBallId;
        private int _secondNextBallId;
        private GameObject _currentBall;
        
        private bool _canDropBall = true;
        
        private void Awake()
        {
            _inputReader = GetComponent<PlayerInputsReader>();
        }

        private void Start()
        {
            _secondNextBallId = GetNextBallId();
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
            var balls = GameManager.Instance.Balls;
            var iconBalls = GameManager.Instance.IconBalls;
            
            _nextBallId = _secondNextBallId;
            
            _currentBall = Instantiate(balls[_nextBallId], _playerBallParent.transform.position, Quaternion.identity, _playerBallParent.transform);
            _ballOffset.Value = _currentBall.GetComponent<CircleCollider2D>().radius * _currentBall.transform.localScale.x;
            
            _secondNextBallId = GetNextBallId();

            for (var i = 0; i < _nextBallParent.transform.childCount; i++)
                Destroy(_nextBallParent.transform.GetChild(i).gameObject);
            
            Instantiate(iconBalls[_secondNextBallId], _nextBallParent.transform.position, Quaternion.identity, _nextBallParent.transform);
        }
        
        private int GetNextBallId()
        {
            return Random.Range(_minBallId, _maxBallId + 1);
        }
        
        private void DropBall()
        {
            if (GameManager.IsGameOver || !_canDropBall) 
                return;
            
            _canDropBall = false;
            _currentBall.transform.SetParent(GameManager.Instance.BallsParent.transform);
            
            var ball = _currentBall.GetComponent<Ball>();
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
