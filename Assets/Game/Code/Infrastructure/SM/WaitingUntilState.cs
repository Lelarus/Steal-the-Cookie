namespace Game.Code.Infrastructure.SM
{
    public class WaitingUntilState : IState
    {
        private readonly PlayerStateMachine _stateMachine;

        public WaitingUntilState(PlayerStateMachine stateMachine)
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