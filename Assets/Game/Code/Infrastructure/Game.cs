using System.Collections.Generic;
using System.Linq;
using Game.Code.Logic;
using UnityEngine;
using TMPro;

namespace Game.Code.Infrastructure
{
    public class Game : MonoBehaviour
    {
        [Header("Player 1")]
        [SerializeField] private Plate[] player1Plates;
        [SerializeField] private RollPlace rollPlace1;
        [SerializeField] private GameObject chooseActionPanel1;
        [SerializeField] private TextMeshProUGUI p1Col1Text;
        [SerializeField] private TextMeshProUGUI p1Col2Text;
        [SerializeField] private TextMeshProUGUI p1Col3Text;

        [Header("Player 2")]
        [SerializeField] private Plate[] player2Plates;
        [SerializeField] private RollPlace rollPlace2;
        [SerializeField] private GameObject chooseActionPanel2;
        [SerializeField] private TextMeshProUGUI p2Col1Text;
        [SerializeField] private TextMeshProUGUI p2Col2Text;
        [SerializeField] private TextMeshProUGUI p2Col3Text;

        private Player _player1;
        private Player _player2;
        
        private Player _activePlayer;

        public static Game Instance;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            
            chooseActionPanel1.SetActive(false);
            chooseActionPanel2.SetActive(false);
            
            _player1 = new Player(this, PlayerType.Player1, player1Plates, rollPlace1, chooseActionPanel1);
            _player2 = new Player(this, PlayerType.Player2, player2Plates, rollPlace2, chooseActionPanel2);
            
            NextPlayerStep();
        }

        private void Update()
        {
            _activePlayer.Update();
        }

        public void CalculateAllScores()
        {
            _player1.CalculateAllScores(p1Col1Text, p1Col2Text, p1Col3Text);
            _player2.CalculateAllScores(p2Col1Text, p2Col2Text, p2Col3Text);
        }

        public void ResultOrNextStep()
        {
            var player1Score = CalculatePlayerScore(player1Plates);
            var player2Score = CalculatePlayerScore(player2Plates);

            if (player1Score + player2Score == 18)
            {
                Result();
            }
            else
            {
                NextPlayerStep();
            }
        }

        public Plate[] GetEnemyColumn(PlayerType playerType, Column column)
        {
            var targetPlayer = playerType == PlayerType.Player1 ? _player2 : _player1;
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
            _activePlayer = _activePlayer == _player1 ? _player2 : _player1;
            _activePlayer.Step();
        }

        private void Result()
        {
            _player1.ToResultState();
            _player2.ToResultState();
        }
    }
}
