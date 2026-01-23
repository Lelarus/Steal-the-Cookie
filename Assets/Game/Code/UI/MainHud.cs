using UnityEngine;

namespace Game.Code.UI
{
    public class MainHud : MonoBehaviour
    {
        [SerializeField] private CanvasGroup resultsPanel;

        private void Awake()
        {
            HideResults();
        }

        public void ShowResults()
        {
            resultsPanel.alpha = 1;
            resultsPanel.blocksRaycasts = true;
        }

        public void HideResults()
        {
            resultsPanel.alpha = 0;
            resultsPanel.blocksRaycasts = false;
        }
    }
}
