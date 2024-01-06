using System;
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
        [SerializeField] private GameObject _ballsParent;

        private GameObject _nextBall;
        private GameObject _currentBall;
        
        private void Awake()
        {
            _inputReader = GetComponent<p_PlayerInputReader>();
        }

        private void Start()
        {
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
            _nextBall = GetNextBall();
            _currentBall = Instantiate(_nextBall, _playerBallParent.transform.position, Quaternion.identity, _playerBallParent.transform);
        }
        
        private GameObject GetNextBall()
        {
            var balls = p_GameManager._instance.SpawnableBalls;
            return balls[Random.Range(0, 5)].ball;
        }

        private void DropBall()
        {
            _currentBall.transform.SetParent(_ballsParent.transform);
            
            var ball = _currentBall.GetComponent<p_Ball>();
            ball.ActiveRigidbody();
            ball.Immpulse();
            
            SpawnNextBall();
        }

        #endregion
    }
}
