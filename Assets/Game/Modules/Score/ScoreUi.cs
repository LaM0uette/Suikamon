using Obvious.Soap;
using TMPro;
using UnityEngine;

namespace Game.Modules.Score
{
    public class ScoreUi : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private IntVariable _score;
        
        private void OnEnable()
        {
            _score.Value = 0;
            _scoreText.text = _score.Value.ToString();
        }
        
        private void Update()
        {
            _scoreText.text = _score.Value.ToString();
        }
    }
}
