using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Code.Logic
{
    public class RollPlace : MonoBehaviour
    {
        [SerializeField] private Sprite[] cookies;
        
        private Plate[] _playerPlates;
        
        private SpriteRenderer _rollPlace;
        
        public int Score { get; private set; }

        private void Awake()
        {
            _rollPlace = GetComponent<SpriteRenderer>();
            _rollPlace.enabled = false;
        }

        public void Generate(Plate[] playerPlates)
        {
            _playerPlates = playerPlates;
            
            Score = Random.Range(1, 7);
            _rollPlace.enabled = true;
            _rollPlace.sprite = GetCookieByScore(Score);

            foreach (var plate in _playerPlates)
            {
                plate.SetActionType(ActionType.Random);
                plate.Enable();
            }
        }
        
        public void Disable()
        {
            Score = 0;
            _rollPlace.enabled = false;
        }
        
        private Sprite GetCookieByScore(int score) => cookies[score - 1];
    }
}
