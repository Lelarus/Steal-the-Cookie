using System.Linq;
using Game.Code.Infrastructure.SM;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace Game.Code.Logic
{
    public class Plate : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject shiningEffect;
        [SerializeField] private TextMeshProUGUI scoreText;

        private Infrastructure.Game _game;
        private PlayerType _playerType;
        private PlayerStateMachine _stateMachine;
        private RollPlace _rollPlace;
        private Plate[] _playerPlates;
        
        private bool _canSet;

        public bool Filled { get; private set; }
        public int Score { get; private set; }
        
        public Column Column { get; set; }

        public void Construct(Infrastructure.Game game, PlayerType playerType, PlayerStateMachine stateMachine, RollPlace rollPlace, Plate[] playerPlates)
        {
            _game = game;
            _playerType = playerType;
            _stateMachine = stateMachine;
            _rollPlace = rollPlace;
            _playerPlates = playerPlates;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_canSet || Filled)
            {
                return;
            }

            Filled = true;
            
            Score = _rollPlace.Score;
            scoreText.text = Score.ToString();

            var enemyColumn = _game.GetEnemyColumn(_playerType, Column);
            enemyColumn.Where(plate => plate.Score == Score).ToList().ForEach(plate => plate.Clear());
            
            _game.CalculateAllScores();

            foreach (var plate in _playerPlates)
            {
                plate.Disable();
            }
            
            _rollPlace.Disable();
            
            _stateMachine.Enter<WaitingUntilState>();
            
            Infrastructure.Game.Instance.ResultOrNextStep();
        }

        public void Enable()
        {
            _canSet = true;

            if (!Filled)
            {
                shiningEffect.SetActive(true);
            }
        }

        private void Disable()
        {
            _canSet = false;
            shiningEffect.SetActive(false);
        }

        private void Clear()
        {
            Score = 0;
            scoreText.text = "-";
            
            Filled = false;
        }
    }
}
