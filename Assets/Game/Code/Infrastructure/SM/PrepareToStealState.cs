using System.Linq;

namespace Game.Code.Infrastructure.SM
{
    public class PrepareToStealState : IState
    {
        private readonly PlayerStateMachine _stateMachine;

        public PrepareToStealState(PlayerStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter(object param = null)
        {
            var oppositePlayer = Game.Instance.GetOppositePlayer();
            var oppositePlates = oppositePlayer
                .Column1
                .Concat(oppositePlayer.Column2)
                .Concat(oppositePlayer.Column3)
                .Where(op => op.Filled);

            foreach (var cake in oppositePlates)
            {
                cake.SetActionType(ActionType.Steal);
                cake.Enable();
            }
        }

        public void Update()
        {
            
        }

        public void Exit(object param = null)
        {
            
        }
    }
}