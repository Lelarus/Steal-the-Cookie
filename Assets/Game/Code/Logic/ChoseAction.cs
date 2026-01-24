using Game.Code.Infrastructure.SM;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Code.Logic
{
    public class ChoseAction : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private ActionType actionType;
        [SerializeField] private GameObject pressedAction;

        public bool Active { get; set; } = true;

        private ChooseActionPanel _chooseActionPanel;
        private PlayerStateMachine _stateMachine;
        
        public void Construct(ChooseActionPanel chooseActionPanel, PlayerStateMachine stateMachine)
        {
            _chooseActionPanel = chooseActionPanel;
            _stateMachine = stateMachine;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!Active)
            {
                return;
            }
            
            if (actionType == ActionType.Random)
            {
                _stateMachine.Enter<RollDiceState>();
            }
            else if (actionType == ActionType.Steal)
            {
                _stateMachine.Enter<PrepareToStealState>();
            }
            
            gameObject.SetActive(false);
            pressedAction.SetActive(true);
            
            _chooseActionPanel.DisableButton();
            
            Invoke(nameof(HidePaper), 0.4f);
        }

        private void HidePaper()
        {
            Infrastructure.Game.Instance.ToggleFade(false);
            _chooseActionPanel.Hide();
        }
    }
}
