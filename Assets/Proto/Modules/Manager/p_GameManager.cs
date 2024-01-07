using Proto.Modules.Balls;
using UnityEngine;

namespace Proto.Modules.Manager
{
    public class p_GameManager : MonoBehaviour
    {
        #region Statements

        public static p_GameManager Instance { get; private set; }
        public static bool IsGameOver { get; set; }
        public static float BallOffset { get; set; }
        
        public GameObject BallsParent;

        public GameObject[] Balls;

        private void Awake()
        {
            Instance ??= this;
        }

        #endregion

        #region Functions

        public void GameOver()
        {
            Debug.Log("Game Over");
            IsGameOver = true;

            var balls = BallsParent.GetComponentsInChildren<p_Ball>();
            foreach (var ball in balls)
            {
                ball.DeactiveRigidbody();
            }
        }

        #endregion
    }
}
