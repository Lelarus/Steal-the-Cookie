using UnityEngine;

namespace Game.Code.UI
{
    public class MainHud : MonoBehaviour
    {
        [SerializeField] private CanvasGroup resultsPanel;
        [SerializeField] private Animator winner;
        
        private static readonly int Blue = Animator.StringToHash("Blue");
        private static readonly int Red = Animator.StringToHash("Red");

        private void Awake()
        {
            HideResults();
        }

        public void ShowResults(PlayerType playerType)
        {
            resultsPanel.alpha = 1;
            resultsPanel.blocksRaycasts = true;
            
            Infrastructure.Game.Instance.ToggleFade(true);
            
            if (playerType == PlayerType.BluePlayer)
            {
                winner.SetTrigger(Blue);
            }
            else if (playerType == PlayerType.RedPlayer)
            {
                winner.SetTrigger(Red);
            }
        }

        public void HideResults()
        {
            resultsPanel.alpha = 0;
            resultsPanel.blocksRaycasts = false;
            
            Infrastructure.Game.Instance.ToggleFade(false);
        }
    }
}
