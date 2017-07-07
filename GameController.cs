using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveThousandStrat
{
    class GameController
    {
        public Dictionary<List<int>, int> SavedDice;

        public List<int> CurrentDice;

        private IScore _scorer;
        private IStrat _strat;

        /// <inheritdoc />
        public GameController(IScore scorer, IStrat strat)
        {
            _scorer = scorer;
            _strat = strat;
            _strat.Initialize(_scorer);
        }

        public void RollDice()
        {
            if(CurrentDice.Count == 0)
                CurrentDice = new List<int> {0, 0, 0, 0, 0, 0};

            var rng = new Random();
            var roll = new List<int>();
            for (int i = 0; i < CurrentDice.Count; i++)
            {
                roll.Add(rng.Next(1, 6));
            }
            roll.Sort();
            CurrentDice = roll;
        }

        public void SaveDice(List<int> list)
        {
            var newList = new List<int>();
            foreach (var i in list)
            {
                CurrentDice.Remove(i);
                newList.Add(i);
            }
            SavedDice[newList] = _scorer.Score(newList);
        }

        public int TotalScore()
        {
            int val = 0;
            foreach (var savedDie in SavedDice)
            {
                val += savedDie.Value;
            }
            return val;
        }

        public int PlayGame()
        {
            SavedDice = new Dictionary<List<int>, int>();
            CurrentDice = new List<int>();
            _strat.Reset();

           
            while (true)
            {
                RollDice();
                Console.WriteLine(String.Join(",", CurrentDice));

                if (_scorer.Score(CurrentDice) == 0)
                    return 0;

                SaveDice(_strat.SaveItems(CurrentDice));

                if (_strat.WillIQuit(CurrentDice))
                    return TotalScore();
            }
        }
    }
}
