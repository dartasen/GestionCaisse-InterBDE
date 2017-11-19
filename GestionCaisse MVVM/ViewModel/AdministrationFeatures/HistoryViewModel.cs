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

        private void OnPropertyChanged([CallerMemberName] string p = null) => PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(p));

        public HistoryViewModel()
        {
            _dateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            _dateTo = DateTime.Now;
            UpdateHistory();
        }

        private void UpdateHistory()
        {
            _history = ProductService.GetHistory(_dateFrom, _dateTo).OrderByDescending(x => x.SaleDate).ToList();
            OnPropertyChanged(nameof(History));
        }

        #region Properties

        private List<ProductService.HistoryQueryResult> _history;

        public List<ProductService.HistoryQueryResult> History
        {
            get => _history;
            set => _history = value;
        }

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
    }
}
