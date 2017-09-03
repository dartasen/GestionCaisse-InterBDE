using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.ViewModel
{
    public class RankingPlayersBySellsViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public RankingPlayersBySellsViewModel()
        {
            _rankedListOfUsersBySellsList = UserService.RankUsersBySellsForAMonth(DateTime.Now.Month);
            var list = _rankedListOfUsersBySellsList.ToList();
            int i = list.Count;

            if (i > 1 && list[0].Quantity > 0)
            {
                FirstUser = $"{list[0].Username} ({list[0].Quantity})";
                OnPropertyChanged(nameof(FirstUser));
            }


            if (i >= 2 && list[1].Quantity > 0)
            {
                SecondUser = $"{list[1].Username} ({list[1].Quantity})";
                OnPropertyChanged(nameof(SecondUser));
            }


            if (i >= 3 && list[2].Quantity > 0)
            {
                ThirdUser = $"{list[2].Username} ({list[2].Quantity})";
                OnPropertyChanged(nameof(ThirdUser));
            }
        }

        #region Properties
        private IOrderedEnumerable<UserService.UserRankQueryResult> _rankedListOfUsersBySellsList;

        public IOrderedEnumerable<UserService.UserRankQueryResult> RankedListOfUsersBySells
        {
            get { return _rankedListOfUsersBySellsList; }
            set { _rankedListOfUsersBySellsList = value; OnPropertyChanged();}
        }


        private string _firstUser;

        public string FirstUser
        {
            get { return _firstUser; }
            set { _firstUser = value; }
        }

        private string _secondUser;

        public string SecondUser
        {
            get { return _secondUser; }
            set { _secondUser = value; }
        }

        private string _thirsUser;

        public string ThirdUser
        {
            get { return _thirsUser; }
            set { _thirsUser = value; }
        }


        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string p = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
    }
}
