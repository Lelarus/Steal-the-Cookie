namespace Game.Code.Infrastructure.SM
{
    public class ResultsState : IState
    {
        private readonly PlayerStateMachine _stateMachine;

        public ResultsState(PlayerStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter(object param = null)
        {
            
        }

        public void Update()
        {
            
        }

        public void Exit(object param = null)
        {
            
        }
    }
}