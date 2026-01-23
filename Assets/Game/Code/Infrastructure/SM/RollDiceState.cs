using Game.Code.Logic;

namespace Game.Code.Infrastructure.SM
{
    public class RollDiceState : IState
    {
        private readonly PlayerStateMachine _stateMachine;
        private readonly Plate[] _playerPlates;
        private readonly RollPlace _rollPlace;

        public RollDiceState(PlayerStateMachine stateMachine, Plate[] playerPlates, RollPlace rollPlace)
        {
            _stateMachine = stateMachine;
            _playerPlates = playerPlates;
            _rollPlace = rollPlace;
        }

        public void Enter(object param = null)
        {
            _rollPlace.Generate(_playerPlates);
        }

        public void Update()
        {
            
        }

        public void Exit(object param = null)
        {
            
        }
    }
}