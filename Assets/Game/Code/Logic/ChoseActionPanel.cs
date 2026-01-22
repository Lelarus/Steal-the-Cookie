using Game.Code.Infrastructure.SM;
using UnityEngine;

namespace Game.Code.Logic
{
    public class ChoseActionPanel : MonoBehaviour
    {
        [SerializeField] private ChoseAction random;
        [SerializeField] private ChoseAction steal;

        public void Construct(PlayerStateMachine stateMachine)
        {
            random.Construct(stateMachine);
            steal.Construct(stateMachine);
        }
    }
}
