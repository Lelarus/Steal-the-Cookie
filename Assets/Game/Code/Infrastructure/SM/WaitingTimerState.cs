namespace Game.Code.Infrastructure.SM
{
    public class WaitingTimerState : IState
    {
        private readonly PlayerStateMachine _stateMachine;

        public WaitingTimerState(PlayerStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter(object param = null)
        {
            var timer = (float)param!;
        }

        public void Update()
        {
            
        }

        public void Exit(object param = null)
        {
            
        }
    }
}