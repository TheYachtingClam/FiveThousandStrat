using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveThousandStrat
{
    class Program
    {
        static void Main(string[] args)
        {
            var scorer = new FiveThousandScorer();
            var strat = new FiveThousandWuss();
            var controller = new GameController(scorer, strat);
            var statCollection = new List<GameStats>();
            for(int i = 0;i < 10000;i++)
            {
                var roundScore = controller.PlayGame();
                statCollection.Add(roundScore);
            }

            Console.WriteLine("AvgRoundScore: {0}", statCollection.Average<GameStats>(s => s.AvgScore));
            Console.ReadKey();
        }
    }
}
