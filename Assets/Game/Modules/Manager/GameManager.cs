using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Modules.Manager
{
    public class GameManager : MonoBehaviour
    {
        #region Statements

        public static GameManager Instance { get; private set; }
        public static bool IsGameOver { get; set; }
        
        [Space, Title("Balls")]
        public GameObject[] Balls;
        [FormerlySerializedAs("IconsBalls")] public GameObject[] IconBalls;
        
        [Space, Title("Parents")]
        public GameObject BallsParent;
        
        private void Awake()
        {
            Instance ??= this;
        }
        
        private void Start()
        {
            LockCursor();
        }

        #endregion

        #region Functions
        
        #region Cursor

        private static void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
        
        private static void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        #endregion

        public void GameOver()
        {
            Debug.Log("Game Over");
            IsGameOver = true;
        }

        #endregion
    }
}
