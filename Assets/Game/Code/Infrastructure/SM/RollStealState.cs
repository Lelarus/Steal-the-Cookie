using Game.Code.Logic;
using UnityEngine;

namespace Game.Code.Infrastructure.SM
{
    public class RollStealState : IState
    {
        private readonly PlayerStateMachine _stateMachine;

        public RollStealState(PlayerStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter(object param = null)
        {
            var plate = (Plate)param;
            var plateScore = plate!.Score;
            var cakeToSteal = Random.Range(1, 7);
            
            Game.Instance.ToggleFade(true);

            Game.Instance.RandomCake.NewCake(_stateMachine, plate, plateScore, cakeToSteal);
        }

        public void Update()
        {
            
        }

        public void Exit(object param = null)
        {
            
        }
    }
}