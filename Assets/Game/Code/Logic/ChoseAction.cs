using Game.Code.Infrastructure.SM;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Code.Logic
{
    public class ChoseAction : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private ActionType actionType;
        
        private PlayerStateMachine _stateMachine;
        
        public void Construct(PlayerStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (actionType == ActionType.Random)
            {
                _stateMachine.Enter<RollDiceState>();
            }
            else if (actionType == ActionType.Steal)
            {
                _stateMachine.Enter<PrepareToStealState>();
            }
        }
    }
}
