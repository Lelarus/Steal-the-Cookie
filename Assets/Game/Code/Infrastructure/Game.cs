using System.Collections.Generic;
using System.Linq;
using Game.Code.Logic;
using Game.Code.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Game.Code.Infrastructure
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private GameObject fade;
        [SerializeField] private RandomCake randomCake;
        [SerializeField] private MainHud mainHud;
        
        [Header("Blue player")]
        [SerializeField] private SpriteRenderer blueMark;
        [SerializeField] private Plate[] bluePlates;
        [SerializeField] private RollPlace rollPlaceBlue;
        [SerializeField] private GameObject choosePaperBlue;
        [SerializeField] private TextMeshProUGUI player1TotalScore;
        [SerializeField] private TextMeshProUGUI blueCol1Text;
        [SerializeField] private TextMeshProUGUI blueCol2Text;
        [SerializeField] private TextMeshProUGUI blueCol3Text;

        [Header("Red player")]
        [SerializeField] private SpriteRenderer redMark;
        [SerializeField] private Plate[] redPlates;
        [SerializeField] private RollPlace rollPlaceRed;
        [SerializeField] private GameObject choosePaperRed;
        [SerializeField] private TextMeshProUGUI player2TotalScore;
        [SerializeField] private TextMeshProUGUI redCol1Text;
        [SerializeField] private TextMeshProUGUI redCol2Text;
        [SerializeField] private TextMeshProUGUI redCol3Text;

        private Player _bluePlayer;
        private Player _redPlayer;
        private Player _activePlayer;

        public RandomCake RandomCake => randomCake;
        public MainHud MainHud => mainHud;
        public Player ActivePlayer => _activePlayer;

        public static Game Instance;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            
            choosePaperBlue.SetActive(false);
            choosePaperRed.SetActive(false);
            
            _bluePlayer = new Player(this, PlayerType.RedPlayer, bluePlates, rollPlaceBlue, choosePaperBlue);
            _redPlayer = new Player(this, PlayerType.BluePlayer, redPlates, rollPlaceRed, choosePaperRed);
            
            NextPlayerStep();
        }

        private void Update()
        {
            _activePlayer.Update();
        }

        public void CalculateAllScores()
        {
            var bluePlayerScore = _bluePlayer.CalculateAllScores(blueCol1Text, blueCol2Text, blueCol3Text);
            var redPlayerScore = _redPlayer.CalculateAllScores(redCol1Text, redCol2Text, redCol3Text);

            player1TotalScore.text = bluePlayerScore.ToString();
            player2TotalScore.text = redPlayerScore.ToString();
        }

        public void ResultOrNextStep()
        {
            var blueScore = CalculatePlayerScore(bluePlates);
            var redScore = CalculatePlayerScore(redPlates);

            if (blueScore == 9)
            {
                Result(PlayerType.BluePlayer);
            }
            else if (redScore == 9)
            {
                Result(PlayerType.RedPlayer);
            }
            else
            {
                NextPlayerStep();
            }
        }

        public IEnumerable<Plate> GetEnemyColumn(PlayerType playerType, Column column)
        {
            var targetPlayer = playerType == PlayerType.RedPlayer ? _redPlayer : _bluePlayer;
            var enemyColumn = column switch
            {
                Column.Column1 => targetPlayer.Column1,
                Column.Column2 => targetPlayer.Column2,
                _ => targetPlayer.Column3
            };

            return enemyColumn;
        }
        
        public Player GetOppositePlayer() => _activePlayer == _bluePlayer ? _redPlayer : _bluePlayer;

        public void ToggleFade(bool toggle) => fade.SetActive(toggle);

        private int CalculatePlayerScore(IEnumerable<Plate> plates) => plates.Where(p => p.Filled).ToList().Count;

        private void NextPlayerStep()
        {
            _activePlayer = GetOppositePlayer();
            _activePlayer.Step();
            
            redMark.enabled = _activePlayer == _redPlayer;
            blueMark.enabled = _activePlayer == _bluePlayer;
        }

        private void Result(PlayerType winner)
        {
            _bluePlayer.ToResultState();
            _redPlayer.ToResultState();

            mainHud.ShowResults(winner);
        }

        public void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
