using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bowling.Classes
{
    class Game : BaseViewModel
    {

        private string inputText = "";
        Random rand = new Random();
        private List<int> tempThrowsList = new List<int>();
        public class GameData
        {
            public int RoundsNumber;
            public int[] Throws;        
        }

        //private List<int> Scores = new List<int>();
        private int tempSum = 0;
        private GameData GameResulte = new GameData();

        public GameData inputGameData(string parInputText)
        {
            inputText = parInputText;
            if (string.IsNullOrEmpty(inputText))
            {
                DataRandomInput();
            }
            else
            {
                var r = inputText.Split(':');
                GameResulte.RoundsNumber = Convert.ToInt32(r[0]);
                GameResulte.Throws = r[1].Split(',').Select(x => int.Parse(x)).ToArray();
            }
            return GameResulte;
        }
        
        private GameData DataRandomInput()
        {
            GameResulte.RoundsNumber = rand.Next(1,11);

            for (int i = 0; i < GameResulte.RoundsNumber; i++)
            {
                RoundRamdomInput(i);
            }
            GameResulte.Throws = tempThrowsList.ToArray();
            
            return GameResulte;            
            
        }
        public string GetInputString(GameData parGD)
        {
            return string.Concat(parGD.RoundsNumber, ':', string.Join(",", parGD.Throws));
        }

        private void RoundRamdomInput(int i)
        {
            int sumRound = rand.Next(4, 11);
            int firstThrow = sumRound - rand.Next(sumRound);
            int secondThrow = sumRound - firstThrow;
            if (firstThrow == 10)
            { 
                tempThrowsList.Add(firstThrow);
                if (i == 9 || i == GameResulte.RoundsNumber - 1)
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
                if (sumRound == 10 && (i == 9 || i == GameResulte.RoundsNumber - 1)) tempThrowsList.Add(rand.Next(11));
            }
        }


        public List<int> Calculation(GameData parGameResulte)
        {
            List<int> Scores = new List<int>();
            for (int i = 0; (i < parGameResulte.Throws.Length) && (Scores.Count < parGameResulte.RoundsNumber); i++)
            {
                tempSum += parGameResulte.Throws[i] + parGameResulte.Throws[i + 1];
                if (parGameResulte.Throws[i] + parGameResulte.Throws[i + 1] <= 10)
                {
                    if (parGameResulte.Throws[i] + parGameResulte.Throws[i + 1] == 10)
                        if (parGameResulte.Throws[i] == 10) { Bonus(i + 2); i -= 1; }
                        else Bonus(i + 2);

                    i += 1;
                }
                else Bonus(i + 2);
                Scores.Add(tempSum);
            }
            return Scores;
        }

        private void Bonus(int nextThrow)
        {
            tempSum += GameResulte.Throws[nextThrow];
        }

        public string outputGameData(IEnumerable<int> Scores)
        {
            List<string> tempScores = new List<string>();
            tempScores = Scores.Select(x => x.ToString()).ToList();
            var outputText = string.Join(",", Scores);
            //OutputText = outputText;
            return outputText;
        }
        public void cleanData()
        {
            inputText = "";
            tempThrowsList = new List<int>();
            //Scores = new List<int>();
            tempSum = 0;
            GameResulte = new GameData();
        }
    }
}
