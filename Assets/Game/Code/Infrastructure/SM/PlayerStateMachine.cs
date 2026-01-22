using System;
using System.Collections.Generic;
using Game.Code.Logic;
using UnityEngine;

namespace Game.Code.Infrastructure.SM
{
    public class PlayerStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        public IState ActiveState;

        public PlayerStateMachine(Plate[] playerPlates, RollPlace rollPlace, GameObject chooseActionPanel)
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(WaitingUntilState)] = new WaitingUntilState(this),
                [typeof(WaitingTimerState)] = new WaitingTimerState(this),
                [typeof(ChooseActionState)] = new ChooseActionState(this, chooseActionPanel),
                [typeof(RollDiceState)] = new RollDiceState(this, playerPlates, rollPlace),
                [typeof(PrepareToStealState)] = new PrepareToStealState(this),
                [typeof(RollStealState)] = new RollStealState(this),
                [typeof(ResultsState)] = new ResultsState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IState
        {
            ActiveState?.Exit();

            var state = GetState<TState>();
            ActiveState = state;
            
            Debug.Log(state);

            return state;
        }

        private TState GetState<TState>() where TState : class, IState =>
            _states[typeof(TState)] as TState;
    }
}