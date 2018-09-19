using GestionCaisse_MVVM.Model.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GestionCaisse_MVVM.ViewModel.AdministrationFeatures
{
    public class HistoryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public HistoryViewModel()
        {
            _dateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            _dateTo = DateTime.Now;
            UpdateHistory();
        }

        private void UpdateHistory()
        {
            History = ProductService.GetHistory(_dateFrom, _dateTo).OrderByDescending(x => x.IdVente).ToList();
            OnPropertyChanged(nameof(History));
        }

        #region Properties
        public List<QueryHistory> History { get; set; }

        private DateTime _dateFrom;

        public DateTime DateFrom
        {
            get => _dateFrom;
            set { _dateFrom = value; OnPropertyChanged(); UpdateHistory(); }
        }

        private DateTime _dateTo;

        public DateTime DateTo
        {
            get => _dateTo;
            set { _dateTo = value; OnPropertyChanged(); UpdateHistory(); }
        }
        #endregion

        private void OnPropertyChanged([CallerMemberName] string p = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
    }
}
