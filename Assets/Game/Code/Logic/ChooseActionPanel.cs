using Game.Code.Infrastructure.SM;
using UnityEngine;

namespace Game.Code.Logic
{
    public class ChooseActionPanel : MonoBehaviour
    {
        [SerializeField] private ChoseAction random;
        [SerializeField] private ChoseAction steal;
        [Space]
        [SerializeField] private GameObject kept;
        [SerializeField] private GameObject stole;

        private Animator _animator;

        private static readonly int Open1 = Animator.StringToHash("Open");
        private static readonly int Hide1 = Animator.StringToHash("Hide");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Construct(PlayerStateMachine stateMachine)
        {
            random.Construct(this, stateMachine);
            steal.Construct(this, stateMachine);
        }

        public void ResetButtons()
        {
            random.Active = true;
            steal.Active = true;
            
            random.gameObject.SetActive(true);
            steal.gameObject.SetActive(true);
            
            kept.SetActive(false);
            stole.SetActive(false);
        }

        public void DisableButton()
        {
            random.Active = false;
            steal.Active = false;
        }

        public void Open() => _animator.SetTrigger(Open1);
        public void Hide() => _animator.SetTrigger(Hide1);
    }
}
