using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace Game.Code.Logic
{
    public class RollPlace : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        
        private Plate[] _playerPlates;
        
        private bool _active;
        
        public int Score { get; private set; }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_active)
            {
                return;
            }
            
            Score = Random.Range(1, 7);
            scoreText.text = Score.ToString();

            foreach (var plate in _playerPlates)
            {
                plate.Enable();
            }

            _active = false;
        }

        public void Enable(Plate[] playerPlates)
        {
            _playerPlates = playerPlates;
            
            _active = true;
        }
        
        public void Disable()
        {
            Score = 0;
            scoreText.text = "-";
            
            _active = false;
        }
    }
}
