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
            var straight = new List<int> {1, 2, 3, 4, 5, 6};

            if (comp(straight, list))
            {
                return 2000;
            }

            if (list[0] == list[1] && list[2] == list[3] && list[4] == list[5])
            {
                return 1500;
            }

            var numbers = new List<int> {0, 0, 0, 0, 0, 0};
            var currentScore = 0;

            numbers[0] = list.Count(s => s == 1);
            numbers[1] = list.Count(s => s == 2);
            numbers[2] = list.Count(s => s == 3);
            numbers[3] = list.Count(s => s == 4);
            numbers[4] = list.Count(s => s == 5);
            numbers[5] = list.Count(s => s == 6);

            if (numbers[0] == 6)
                return 2000;
            if (numbers[1] == 6)
                return 400;
            if (numbers[2] == 6)
                return 600;
            if (numbers[3] == 6)
                return 800;
            if (numbers[4] == 6)
                return 1000;
            if (numbers[5] == 6)
                return 1200;

            currentScore += numbers[0] / 3 * 1000 + numbers[0] % 3 * 100;
            currentScore += numbers[1] / 3 * 200;
            currentScore += numbers[2] / 3 * 300;
            currentScore += numbers[3] / 3 * 400;
            currentScore += numbers[4] / 3 * 500 + numbers[4] % 3 * 50;
            currentScore += numbers[5] / 3 * 600;

            return currentScore;
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
