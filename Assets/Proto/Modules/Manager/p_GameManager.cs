using UnityEngine;

namespace Proto.Modules.Manager
{
    public class p_GameManager : MonoBehaviour
    {
        #region Statements

        public static p_GameManager Instance { get; private set; }
        public static float BallOffset { get; set; }
        
        public GameObject BallsParent;

        public GameObject[] Balls;

        private void Awake()
        {
            Instance ??= this;
        }

        #endregion
    }
}
