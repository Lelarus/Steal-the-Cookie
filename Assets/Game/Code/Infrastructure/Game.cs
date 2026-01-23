using System.Collections.Generic;
using System.Linq;
using Game.Code.Logic;
using Game.Code.UI;
using UnityEngine;
using TMPro;

namespace Game.Code.Infrastructure
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private MainHud mainHud;
        
        [Header("Blue player")]
        [SerializeField] private Plate[] bluePlates;
        [SerializeField] private RollPlace rollPlaceBlue;
        [SerializeField] private GameObject choosePaperBlue;
        [SerializeField] private TextMeshProUGUI player1TotalScore;
        [SerializeField] private TextMeshProUGUI blueCol1Text;
        [SerializeField] private TextMeshProUGUI blueCol2Text;
        [SerializeField] private TextMeshProUGUI blueCol3Text;

        [Header("Red player")]
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

        public MainHud MainHud => mainHud;

        public static Game Instance;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            
            choosePaperBlue.SetActive(false);
            choosePaperRed.SetActive(false);
            
            _bluePlayer = new Player(this, PlayerType.Player1, bluePlates, rollPlaceBlue, choosePaperBlue);
            _redPlayer = new Player(this, PlayerType.Player2, redPlates, rollPlaceRed, choosePaperRed);
            
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

            if (blueScore == 9 || redScore == 9)
            {
                Result();
            }
            else
            {
                NextPlayerStep();
            }
        }

        public IEnumerable<Plate> GetEnemyColumn(PlayerType playerType, Column column)
        {
            var targetPlayer = playerType == PlayerType.Player1 ? _redPlayer : _bluePlayer;
            var enemyColumn = column switch
            {
                Column.Column1 => targetPlayer.Column1,
                Column.Column2 => targetPlayer.Column2,
                _ => targetPlayer.Column3
            };

            return enemyColumn;
        }

        private int CalculatePlayerScore(IEnumerable<Plate> plates) => plates.Where(p => p.Filled).ToList().Count;

        private void NextPlayerStep()
        {
            _activePlayer = _activePlayer == _bluePlayer ? _redPlayer : _bluePlayer;
            _activePlayer.Step();
        }

        private void Result()
        {
            _bluePlayer.ToResultState();
            _redPlayer.ToResultState();
        }
    }
}
