using System.Collections.Generic;
using System.Linq;
using Game.Code.Infrastructure.SM;
using Game.Code.Logic;
using TMPro;
using UnityEngine;

namespace Game.Code.Infrastructure
{
    public class Player
    {
        private readonly PlayerStateMachine _stateMachine;
        
        public readonly Plate[] Column1, Column2, Column3;
        
        public Player(Game game, PlayerType playerType, Plate[] playerPlates, RollPlace rollPlace, GameObject chooseActionPanel)
        {
            _stateMachine = new PlayerStateMachine(playerPlates, rollPlace, chooseActionPanel);
            _stateMachine.Enter<WaitingUntilState>();
            
            Column1 = new[] { playerPlates[0], playerPlates[3], playerPlates[6] };
            Column2 = new[] { playerPlates[1], playerPlates[4], playerPlates[7] };
            Column3 = new[] { playerPlates[2], playerPlates[5], playerPlates[8] };
            
            SetColumn(Column1, Column.Column1);
            SetColumn(Column2, Column.Column2);
            SetColumn(Column3, Column.Column3);
            
            foreach (var playerPlate in playerPlates)
            {
                playerPlate.Construct(game, playerType, _stateMachine, rollPlace, playerPlates);
            }
        }

        public void Update()
        {
            _stateMachine.ActiveState.Update();
        }

        public void Step()
        {
            _stateMachine.Enter<ChooseActionState>();
        }

        public void ToResultState()
        {
            _stateMachine.Enter<ResultsState>();
        }

        public void CalculateAllScores(TextMeshProUGUI col1, TextMeshProUGUI col2, TextMeshProUGUI col3)
        {
            col1.text = CalculateScore(Column1).ToString();
            col2.text = CalculateScore(Column2).ToString();
            col3.text = CalculateScore(Column3).ToString();
        }

        private void SetColumn(IEnumerable<Plate> plates, Column column)
        {
            foreach (var plate in plates)
            {
                plate.Column = column;
            }
        }

        private int CalculateScore(Plate[] column)
        {
            var groups = column
                .Select(c => c.Score)
                .GroupBy(v => v)
                .Select(g => new { Value = g.Key, Count = g.Count() })
                .ToList();

            if (groups.Count == 3)
            {
                return column.Sum(c => c.Score);
            }

            if (groups.Count == 2)
            {
                var result = 0;

                foreach (var g in groups)
                {
                    if (g.Count == 2)
                        result += g.Value * 2 * 2; // (a + a) * 2
                    else
                        result += g.Value;
                }

                return result;
            }

            if (groups.Count == 1)
            {
                return column.Sum(c => c.Score) * 3;
            }

            return 0;
        }
    }
}
