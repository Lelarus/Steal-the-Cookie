using System.Linq;
using Game.Code.Infrastructure.SM;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Code.Logic
{
    public class Plate : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private SpriteRenderer cookiePlace;
        [SerializeField] private Sprite[] cookies;

        private Infrastructure.Game _game;
        private PlayerType _playerType;
        private PlayerStateMachine _stateMachine;
        private RollPlace _rollPlace;
        private Plate[] _playerPlates;
        
        private bool _canSet;
        private ActionType _actionType;

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
            if (_actionType == ActionType.Random)
            {
                if (!_canSet || Filled)
                {
                    return;
                }
                
                Filled = true;
                
                Score = _rollPlace.Score;
                cookiePlace.sprite = GetCookieByScore(Score);
                cookiePlace.enabled = true;
                
                var enemyColumn = _game.GetEnemyColumn(_playerType, Column);
                enemyColumn.Where(plate => plate.Score == Score).ToList().ForEach(plate => plate.Clear());
                
                _game.CalculateAllScores();

                foreach (var plate in _playerPlates)
                {
                    plate.Disable();
                }
            
                _rollPlace.Disable();
            
                Invoke(nameof(NextStepAfterRandom), 1.5f);
            }
            else if (_actionType == ActionType.Steal)
            {
                if (!_canSet || !Filled)
                {
                    return;
                }
                
                foreach (var plate in _playerPlates)
                {
                    plate.Disable();
                }
                
                _stateMachine.Enter<RollStealState>(this);
            }
        }

        public void Enable()
        {
            _canSet = true;
        }

        public void SetActionType(ActionType actionType) => _actionType = actionType;

        private Sprite GetCookieByScore(int score) => cookies[score - 1];

        public void Disable()
        {
            _canSet = false;
        }

        private void NextStepAfterRandom()
        {
            _stateMachine.Enter<WaitingUntilState>();
            Infrastructure.Game.Instance.ResultOrNextStep();
        }

        public void Clear()
        {
            Score = 0;
            cookiePlace.enabled = false;
            
            Filled = false;
        }
    }
}
