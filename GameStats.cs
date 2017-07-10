using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveThousandStrat
{
    class GameStats
    {
        public int TotalRounds = 0;

        public int AvgScore = 0;

        public int finalScore = 0;

        public int numberOfSkunks = 0;

        public override string ToString()
        {
            return string.Format("AvgScore: {0}\nTotalRounds: {1}\nFinalScore: {2}\nSkunks: {3}", AvgScore, TotalRounds, finalScore, numberOfSkunks);
        }
    }
}
