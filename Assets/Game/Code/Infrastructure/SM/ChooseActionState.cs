using Game.Code.Logic;
using UnityEngine;

namespace Game.Code.Infrastructure.SM
{
    public class ChooseActionState : IState
    {
        private readonly PlayerStateMachine _stateMachine;
        private readonly GameObject _chooseActionPanel;

        public ChooseActionState(PlayerStateMachine stateMachine, GameObject chooseActionPanel)
        {
            _stateMachine = stateMachine;
            _chooseActionPanel = chooseActionPanel;
            
            _chooseActionPanel.GetComponent<ChoseActionPanel>().Construct(stateMachine);
        }

        public void Enter(object param = null)
        {
            _chooseActionPanel.SetActive(true);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _stateMachine.Enter<RollDiceState>();
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _stateMachine.Enter<PrepareToStealState>();
            }
        }

        public void Exit(object param = null)
        {
            _chooseActionPanel.SetActive(false);
        }
    }
}