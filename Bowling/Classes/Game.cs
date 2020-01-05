using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bowling.Classes
{
    class Game
    {
        Random rand = new Random();

        public class GameData
        {
            public int RoundsNumber;
            public int[] Throws;
        }

        public GameData inputGameData(string parInputText)
        {
            GameData GameResulte = new GameData();
            if (string.IsNullOrEmpty(parInputText))
            {
                GameResulte = DataRandomInput();
            }
            else
            {
                var r = parInputText.Split(':');
                GameResulte.RoundsNumber = Convert.ToInt32(r[0]);
                GameResulte.Throws = r[1].Split(',').Select(x => int.Parse(x)).ToArray();
            }
            return GameResulte;
        }

        private GameData DataRandomInput()
        {
            var tempThrowsList = new List<int>();
            GameData GameResulte = new GameData();
            GameResulte.RoundsNumber = rand.Next(1, 11);

            for (int i = 0; i < GameResulte.RoundsNumber; i++)
            {
                RoundRamdomInput(tempThrowsList, i, GameResulte.RoundsNumber);
            }
            GameResulte.Throws = tempThrowsList.ToArray();

            return GameResulte;

        }
        
        private void RoundRamdomInput(List<int> tempThrowsList, int i, int RoundsNumber)
        {
            int sumRound = rand.Next(4, 11);
            int firstThrow = sumRound - rand.Next(sumRound);
            int secondThrow = sumRound - firstThrow;
            if (firstThrow == 10)
            {
                tempThrowsList.Add(firstThrow);
                if (i == 9 || i == RoundsNumber - 1)
                {
                    sumRound = rand.Next(4, 11);
                    firstThrow = sumRound - rand.Next(sumRound);
                    secondThrow = sumRound - firstThrow;
                    tempThrowsList.Add(firstThrow);
                    tempThrowsList.Add(secondThrow);
                }
            }
            else
            {
                tempThrowsList.Add(firstThrow);
                tempThrowsList.Add(secondThrow);
                if (sumRound == 10 && (i == 9 || i == RoundsNumber - 1)) tempThrowsList.Add(rand.Next(11));
            }
        }
        
        public List<int> Calculation(GameData parGameResulte)
        {
            int tempSum = 0;
            List<int> Scores = new List<int>();
            for (int i = 0; (i < parGameResulte.Throws.Length) && (Scores.Count < parGameResulte.RoundsNumber); i++)
            {
                tempSum += parGameResulte.Throws[i] + parGameResulte.Throws[i + 1];
                if (parGameResulte.Throws[i] + parGameResulte.Throws[i + 1] <= 10)
                {
                    if (parGameResulte.Throws[i] + parGameResulte.Throws[i + 1] == 10)
                        if (parGameResulte.Throws[i] == 10) { tempSum += parGameResulte.Throws[i + 2]; i -= 1; }
                        else tempSum += parGameResulte.Throws[i + 2];

                    i += 1;
                }
                else tempSum += parGameResulte.Throws[i + 2];
                Scores.Add(tempSum);
            }
            return Scores;
        }

        public string GetInputString(GameData parGD)
        {
            return string.Concat(parGD.RoundsNumber, ':', string.Join(",", parGD.Throws));
        }

        public string outputGameData(IEnumerable<int> Scores)
        {
            return string.Join(",", Scores);
        }

    }
}
