using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveThousandStrat
{
    interface IStrat
    {
        void Initialize(IScore score);

        void Reset(int minimumWin);

        List<int> SaveItems(List<int> currentItems);

        bool WillIQuit(List<int> currentItems);
    }
}
