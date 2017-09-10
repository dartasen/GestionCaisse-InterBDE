using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GestionCaisse_MVVM.Model.Entities;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.ViewModel
{
    public class QuickCalcsViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public QuickCalcsViewModel()
        {
            _dues = new List<StringWrapper>();

            CalculateDues = new RelayCommand(Calculate, o => true);
        }

        #region Properties

        private double _priceToShare;

        public double PriceToShare
        {
            get { return _priceToShare; }
            set
            {
                _priceToShare = value;
                OnPropertyChanged();
            }
        }

        private List<StringWrapper> _dues;

        public List<StringWrapper> Dues
        {
            get { return _dues; }
            set { _dues = value; OnPropertyChanged();}
        }


        #endregion

        #region Commands

        public ICommand CalculateDues { get; }

        #endregion

        private void Calculate()
        {
            try
            {
                if (Dues.Count > 0) Dues.Clear();

                var coefficients = BDEService.GetBDEsCoefficients();
                foreach (var coefficient in coefficients)
                {
                    Dues.Add(new StringWrapper
                    {
                        Value =
                            $"{coefficient.Key} ({coefficient.Value * 100} %) : {2 * coefficient.Value}"
                    });
                }
                OnPropertyChanged(nameof(Dues));
            }
            catch (InvalidCastException)
            {
                DialogService s = new DialogService();
                s.ShowInformationWindow("Votre nombre est invalide !", "Calcul impossible !", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string p = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
    }
}