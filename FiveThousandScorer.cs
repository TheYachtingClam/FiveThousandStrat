using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveThousandStrat
{
    using System.Data.SqlTypes;

    class FiveThousandScorer : IScore
    {
        #region Implementation of IScore

        /// <inheritdoc />
        public int Score(List<int> list)
        {
            var score = 0;
            var scoreList = ScoreList(list);
            foreach(var scoreItem in scoreList)
            {
                score += scoreItem.Item2;
            }
            return score;
        }

        public List<Tuple<List<int>, int>> ScoreList(List<int> list)
        {
            var straight = new List<int> { 1, 2, 3, 4, 5, 6 };

            if (comp(straight, list))
            {
                return new List<Tuple<List<int>, int>> { new Tuple<List<int>, int>(straight, 2000) };
            }

            if (list.Count == 6 && list[0] == list[1] && list[2] == list[3] && list[4] == list[5])
            {
                return new List<Tuple<List<int>, int>> { new Tuple<List<int>, int>(list, 1500) };
            }

            var numbers = new List<int> { 0, 0, 0, 0, 0, 0 };
            var threeOnes = new List<int> { 1, 1, 1 };
            var threeTwos = new List<int> { 2, 2, 2 };
            var threeThrees = new List<int> { 3, 3, 3 };
            var threeFours = new List<int> { 4, 4, 4 };
            var threeFives = new List<int> { 5, 5, 5 };
            var threeSixes = new List<int> { 6, 6, 6 };
            var returnValues = new List<Tuple<List<int>, int>>();

            numbers[0] = list.Count(s => s == 1);
            numbers[1] = list.Count(s => s == 2);
            numbers[2] = list.Count(s => s == 3);
            numbers[3] = list.Count(s => s == 4);
            numbers[4] = list.Count(s => s == 5);
            numbers[5] = list.Count(s => s == 6);

            // Ones
            var ones = numbers[0];
            if(numbers[0] == 6)
            {
                returnValues.Add(new Tuple<List<int>, int>(threeOnes, 1000));
                ones -= 3;
            }
            if(numbers[0] == 3)
            {
                returnValues.Add(new Tuple<List<int>, int>(threeOnes, 1000));
                ones -= 3;
            }
            for (int i = 0; i < ones % 3; i++)
            {
                returnValues.Add(new Tuple<List<int>, int>(new List<int> { 1 }, 100));
            }

            // Twos
            if (numbers[1] == 6)
            {
                returnValues.Add(new Tuple<List<int>, int>(threeTwos, 200));
            }
            if (numbers[1] == 3)
            {
                returnValues.Add(new Tuple<List<int>, int>(threeTwos, 200));
            }

            // Threes
            if (numbers[2] == 6)
            {
                returnValues.Add(new Tuple<List<int>, int>(threeThrees, 300));
            }
            if (numbers[2] == 3)
            {
                returnValues.Add(new Tuple<List<int>, int>(threeThrees, 300));
            }

            // Fours
            if (numbers[3] == 6)
            {
                returnValues.Add(new Tuple<List<int>, int>(threeFours, 400));
            }
            if (numbers[3] == 3)
            {
                returnValues.Add(new Tuple<List<int>, int>(threeFours, 400));
            }

            // Fives
            var fives = numbers[4];
            if (numbers[4] == 6)
            {
                returnValues.Add(new Tuple<List<int>, int>(threeFives, 500));
                fives -= 3;
            }
            if (numbers[4] == 3)
            {
                returnValues.Add(new Tuple<List<int>, int>(threeFives, 500));
                fives -= 3;
            }
            for (int i = 0; i < fives % 3; i++)
            {
                returnValues.Add(new Tuple<List<int>, int>(new List<int> { 5 }, 50));
            }
            
            // Fours
            if (numbers[5] == 6)
            {
                returnValues.Add(new Tuple<List<int>, int>(threeSixes, 600));
            }
            if (numbers[5] == 3)
            {
                returnValues.Add(new Tuple<List<int>, int>(threeSixes, 600));
            }
            
            return returnValues;
        }

        private bool comp(List<int> first, List<int> second)
        {
            var firstNotSecond = first.Except(second).ToList();
            var secondNotFirst = second.Except(first).ToList();

            return !firstNotSecond.Any() && !secondNotFirst.Any();
        }

        #endregion
    }
}
