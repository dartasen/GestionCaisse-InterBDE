using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.ViewModel
{
    public class SynthesisViewModel
    {
        public SynthesisViewModel()
        {

        }

        #region Properties
        public IEnumerable<UserService.UserRankQueryResult> RankedUsers
        {
            get { return UserService.RankUsersBySellsForAMonth(DateTime.Now.Month); }
        }

        #endregion
    }
}
