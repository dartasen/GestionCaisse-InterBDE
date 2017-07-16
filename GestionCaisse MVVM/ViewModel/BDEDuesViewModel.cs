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
    public class BDEDuesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string p = null) => PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(p));

        public BDEDuesViewModel()
        {
            _dateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            _dateTo = DateTime.Now;
            UpdateDues();
        }

        private void UpdateDues()
        {
            _bdeDues = BDEService.GetBDEDues(_dateFrom, _dateTo);
            OnPropertyChanged(nameof(BDEDues));
        }

        #region Properties
        private List<BDEService.BDEDue> _bdeDues;

        public List<BDEService.BDEDue> BDEDues
        {
            get { return _bdeDues; }
            set { _bdeDues = value; }
        }

        private DateTime _dateFrom;

        public DateTime DateFrom
        {
            get => _dateFrom;
            set { _dateFrom = value; OnPropertyChanged(); UpdateDues(); }
        }

        private DateTime _dateTo;

        public DateTime DateTo
        {
            get => _dateTo;
            set { _dateTo = value; OnPropertyChanged(); UpdateDues(); }
        }
        #endregion
    }
}
