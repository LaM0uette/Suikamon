using UnityEngine;

namespace Game.Modules.Manager
{
    public class GameManager : MonoBehaviour
    {
        #region Statements

        public static GameManager Instance { get; private set; }
        
        private void Awake()
        {
            Instance ??= this;
        }

        #endregion
    }
}
