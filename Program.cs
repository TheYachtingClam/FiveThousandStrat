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
            controller.PlayGame();
        }
    }
}
