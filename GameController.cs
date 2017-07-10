using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveThousandStrat
{
    class GameController
    {
        public List<Tuple<List<int>, int>> SavedDice;

        public List<int> CurrentDice;

        private IScore _scorer;
        private IStrat _strat;
        private Random rng;

        /// <inheritdoc />
        public GameController(IScore scorer, IStrat strat)
        {
            _scorer = scorer;
            _strat = strat;
            _strat.Initialize(_scorer);
            rng = new Random();
        }

        public void RollDice()
        {
            if(CurrentDice.Count == 0)
                CurrentDice = new List<int> {0, 0, 0, 0, 0, 0};
            
            var roll = new List<int>();
            for (int i = 0; i < CurrentDice.Count; i++)
            {
                var rnVal = rng.Next(1, 6);
                roll.Add(rnVal);
            }
            roll.Sort();
            CurrentDice = roll;
        }

        public void SaveDice(Tuple<List<int>, int> list)
        {
            var newList = new List<int>();
            foreach (var i in list.Item1.ToList())
            {
                CurrentDice.Remove(i);
                newList.Add(i);
            }
            SavedDice.Add(list);
        }

        public int TotalScore()
        {
            int val = 0;
            foreach (var savedDie in SavedDice)
            {
                val += savedDie.Item2;
            }
            return val;
        }

        public int PlayRound(int minWin)
        {
            SavedDice = new List<Tuple<List<int>, int>>();
            CurrentDice = new List<int>();
            _strat.Reset(minWin);

            while (true)
            {
                RollDice();
                //Console.WriteLine("**************************************************");
                //Console.WriteLine("Current Roll: {0}", String.Join(",", CurrentDice));

                if (_scorer.Score(CurrentDice) == 0)
                {
                    //Console.WriteLine("     Skunk Occured, Score: 0", TotalScore());
                    return 0;
                }

                foreach(var savedItem in _strat.SaveItems(CurrentDice))
                {
                    //Console.WriteLine("     Saving ({0}) = {1}", string.Join(",", savedItem.Item1), savedItem.Item2);
                    SaveDice(savedItem);
                }

                //Console.WriteLine("     CurrentScore: {0}", TotalScore());

                var quit = _strat.WillIQuit(CurrentDice);
                var currentDiceCount = CurrentDice.Count;
                //Console.WriteLine("quit = {0}, diceCount = {1}", quit, currentDiceCount);

                if (currentDiceCount != 0 && quit)
                {
                    //Console.WriteLine("total round score = {0}", TotalScore());
                    return TotalScore();
                }
                    
            }
        }

        public GameStats PlayGame()
        {
            var stats = new GameStats();
            var minWin = 500;
            var currentScore = 0;
            var currentRound = 0;
                        
            while(currentScore < 5000)
            {
                currentRound++;
                var roundScore = PlayRound(minWin);
                if(roundScore > 0)
                {
                    minWin = 350;
                }
                else
                {
                    stats.numberOfSkunks++;
                }

                currentScore += roundScore;
            }

            stats.AvgScore = currentScore / currentRound;
            stats.finalScore = currentScore;
            stats.TotalRounds = currentRound;

            return stats;
        }
    }
}
