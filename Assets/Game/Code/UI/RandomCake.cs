using Game.Code.Infrastructure.SM;
using Game.Code.Logic;
using UnityEngine;

namespace Game.Code.UI
{
    public class RandomCake : MonoBehaviour
    {
        [SerializeField] private Sprite[] cakes;
        [SerializeField] private SpriteRenderer target;

        private Animator _animator;
        
        private PlayerStateMachine _stateMachine;
        private Plate _targetPlate;

        private static readonly int Cake = Animator.StringToHash("NewCake");

        private int _plateScore, _cakeToSteal;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void NewCake(PlayerStateMachine stateMachine, Plate targetPlate, int plateScore, int cakeToSteal)
        {
            _stateMachine = stateMachine;
            _targetPlate = targetPlate;
            _plateScore = plateScore;
            _cakeToSteal = cakeToSteal;
            
            _animator.SetTrigger(Cake);
        }

        private void NewCakeSpawnStart()
        {
            target.enabled = false;
        }

        private void NewCakeSpawnEnd()
        {
            target.enabled = true;
            target.sprite = cakes[_cakeToSteal - 1];
            
            Invoke(nameof(NewCakeAction), 1.5f);
        }

        private void NewCakeAction()
        {
            target.enabled = false;
            Infrastructure.Game.Instance.ToggleFade(false);

            if (_plateScore == _cakeToSteal)
            {
                _targetPlate.Clear();
                Infrastructure.Game.Instance.ActivePlayer.AdditionalScore += _plateScore;
            }

            Invoke(nameof(EndSteal), 1.5f);
        }

        private void EndSteal()
        {
            _stateMachine.Enter<WaitingUntilState>();
            
            Infrastructure.Game.Instance.CalculateAllScores();
            Infrastructure.Game.Instance.ResultOrNextStep();
        }
    }
}
