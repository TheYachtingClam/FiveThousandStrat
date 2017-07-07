using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveThousandStrat
{
    //This wuss will only grab big wins and 1's, he will always quit when he can.  500 the first time, and 350 from then on.
    class FiveThousandWuss : IStrat
    {
        private IScore _scorer;
        private int _totalScore;
        private int _minWin;

        #region Implementation of IStrat

        /// <inheritdoc />
        public void Initialize(IScore score)
        {
            _scorer = score;
        }

        /// <inheritdoc />
        public void Reset(int minWin)
        {
            _totalScore = 0;
            _minWin = minWin;
        }

        /// <inheritdoc />
        public List<int> SaveItems(List<int> currentItems)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool WillIQuit(List<int> currentItems)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
