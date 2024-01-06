using System.Collections.Generic;
using UnityEngine;

namespace Proto.Modules.Manager
{
    public class p_GameManager : MonoBehaviour
    {
        #region Statements

        public static p_GameManager _instance { get; private set; }

        [SerializeField] private List<p_BallData> _balls = new();
        public p_BallData[] Balls => _balls.ToArray();
        public p_BallData[] SpawnableBalls => _balls.GetRange(0, 5).ToArray();

        private void Awake()
        {
            _instance ??= this;
        }

        #endregion
    }
}
