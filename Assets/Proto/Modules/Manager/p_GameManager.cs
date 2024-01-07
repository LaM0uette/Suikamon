using UnityEngine;

namespace Proto.Modules.Manager
{
    public class p_GameManager : MonoBehaviour
    {
        #region Statements

        public static p_GameManager _instance { get; private set; }

        public GameObject[] Balls;

        private void Awake()
        {
            _instance ??= this;
        }

        #endregion
    }
}
