using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Modules.Manager
{
    public class GameManager : MonoBehaviour
    {
        #region Statements

        public static GameManager Instance { get; private set; }
        public static bool IsGameOver { get; set; }
        
        [Space, Title("Balls")]
        public GameObject[] Balls;
        
        [Space, Title("Parents")]
        public GameObject BallsParent;
        
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
        }

        #endregion
    }
}
