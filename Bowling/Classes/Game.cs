using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bowling.Classes
{
    class Game
    {
        Random rand = new Random();

        //structure for storing game data
        public class GameData
        {
            public int RoundsNumber;
            public int[] Throws;
        }

        //detecting inputs, converting to the right look
        public GameData inputGameData(string parInputText)
        {
            var GameResulte = new GameData();

            //checking for inputs
            if (string.IsNullOrEmpty(parInputText))
            {
                //filling with random values if there are no inputs
                GameResulte = DataRandomInput();
            }
            else
            {
                //converting inputs
                var r = parInputText.Split(':');
                GameResulte.RoundsNumber = Convert.ToInt32(r[0]);
                GameResulte.Throws = r[1].Split(',').Select(x => int.Parse(x)).ToArray();
            }
            return GameResulte;
        }

        //filling the game data with random values
        private GameData DataRandomInput()
        {
            var tempThrowsList = new List<int>();
            var GameResulte = new GameData();
            var isLastRound = false;

            //generate the number of rounds
            GameResulte.RoundsNumber = rand.Next(1, 11);

            // generate the result of throws for each round
            for (int i = 0; i < GameResulte.RoundsNumber; i++)
            {
                if (i == GameResulte.RoundsNumber - 1) isLastRound = true;
                RoundRamdomInput(tempThrowsList, isLastRound);
            }

            GameResulte.Throws = tempThrowsList.ToArray();

            return GameResulte;

        }

        //generate the result of throws for one round
        private void RoundRamdomInput(List<int> tempThrowsList, bool lastRound)
        {
            //generate the result of throws in this round
            var sumRound = rand.Next(4, 11);
            var firstThrow = sumRound - rand.Next(sumRound);
            var secondThrow = sumRound - firstThrow;

            // push the result of throws
            if (firstThrow == 10)
            {
                tempThrowsList.Add(firstThrow);

                // add bonus throws for the strike in last round
                if (lastRound)
                {
                    //generate the result of bonus throws
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

                //add bonus throw for the spare in last round
                if (sumRound == 10 && (lastRound)) tempThrowsList.Add(rand.Next(11));
            }
        }

        public List<int> Calculation(GameData parGameResulte)
        {
            var tempSum = 0;
            var Scores = new List<int>();

            //cycle by rounds
            for (int i = 0; (i < parGameResulte.Throws.Length) && (Scores.Count < parGameResulte.RoundsNumber); i++)
            {
                /*add to the sum the value of the current throw (the first throw of the round)
                and the next throw (the second throw or in strike the first bonus throw)*/
                tempSum += parGameResulte.Throws[i] + parGameResulte.Throws[i + 1];

                //if not strike (10 + smth)>10
                if (parGameResulte.Throws[i] + parGameResulte.Throws[i + 1] <= 10)
                {
                    //if spare or strike (10 + 0(the first throw of next round))
                    if (parGameResulte.Throws[i] + parGameResulte.Throws[i + 1] == 10)
                    {
                        /*when spare add first bonus throw
                        when strike add second bonus throw*/
                        tempSum += parGameResulte.Throws[i + 2];

                        //in order to not to skip the beginn of the next round
                        if (parGameResulte.Throws[i] == 10) i -= 1;
                    }

                    //go to beginning next round
                    i += 1;
                }
                // when strike add second bonus throw
                else tempSum += parGameResulte.Throws[i + 2];

                Scores.Add(tempSum);
            }
            return Scores;
        }

        //generate string of inputs 
        public string GetInputString(GameData parGD)
        {
            return string.Concat(parGD.RoundsNumber, ':', string.Join(",", parGD.Throws));
        }

        //generate string of outputs
        public string outputGameData(IEnumerable<int> Scores)
        {
            return string.Join(",", Scores);
        }

    }
}
