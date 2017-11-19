using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.ViewModel.AdministrationFeatures
{
    public class QuickCalcsViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public QuickCalcsViewModel()
        {
            PriceToShare = 0.00;

            CalculateDues = new RelayCommand(Calculate, o => true);
        }

        #region Properties
        //TODO ERROR : XAML empêche l'insertion de double 
        private double _priceToShare;

        public double PriceToShare
        {
            get => _priceToShare;
            set
            {
                _priceToShare = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<string> _dues;

        public ObservableCollection<string> Dues
        {
            get => _dues;
            set => _dues = value;
        }
        #endregion

        #region Commands

        public ICommand CalculateDues { get; }

        #endregion

        private void Calculate()
        {
            try
            {
                if (Dues == null)
                    Dues = new ObservableCollection<string>();

                if (Dues.Count > 0)
                {
                    Dues.Clear();
                }

                var coefficients = BDEService.GetBDEsCoefficients();
                foreach (var coefficient in coefficients)
                {
                    Dues.Add($"{coefficient.Key} ({coefficient.Value * 100} %) : {coefficient.Value * PriceToShare}");
                }
                OnPropertyChanged(nameof(Dues));
            }
            catch (InvalidCastException)
            {
                var s = new DialogService();
                s.ShowInformationWindow("Votre nombre est invalide !", "Calcul impossible !", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string p = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
    }
}