using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using GestionCaisse_MVVM.Exceptions;
using GestionCaisse_MVVM.Model.Entities;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.ViewModel
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly BasketService _basketService = BasketService.Instance;
        private readonly LoginService _loginService = LoginService.Instance;

        public MainWindowViewModel()
        {
            var dialogService = new DialogService();

            InsertProduct = new RelayCommand(() => dialogService.ShowProductInsertPage(), actionToCheckExecute: o => true);
            ResetBasket = new RelayCommand(() => _basketService.GetBasket().ResetBasket(), o => true);
            ValidateSell = new RelayCommand(() =>
            {
                try
                {
                    if (_basketService.ValidateSell())
                    {
                        _basketService.GetBasket().ResetBasket();
                        dialogService.ShowInformationWindow(
                            "Vente effectuée !",
                            "Confirmation",
                            MessageBoxButton.OK,
                            MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        dialogService.ShowInformationWindow(
                            "Vente invalide ou impossible !",
                            "Attention",
                            MessageBoxButton.OK,
                            MessageBoxImage.Hand);
                    }
                }
                catch (ConnectionFailedException ex)
                {
                    if (ex.InnerException != null)
                        dialogService.ShowInformationWindow(
                            "Problème de connexion à la base de données !\n" + ex.InnerException.Message,
                            "Connexion impossible !", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, o => true);

            Logout = new RelayCommand(() =>
            {
                _basketService.GetBasket().ResetBasket();
                Close();
            }, o => true);

            //TODO Sélectionner le BDE de l'utilisateur par défaut
            //=> WORKAROUND
            _selectedBDE = LoginService.Instance.GetLoginContext().BuyingBDE;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string p = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }

        #region Commands

        public ICommand ResetBasket { get; }

        public ICommand InsertProduct { get; }

        public ICommand ValidateSell { get; }

        public ICommand Logout { get; }

        #endregion

        #region Properties

        public Basket Basket => BasketService.Instance.GetBasket();

        public IEnumerable<BDE> BDEs
        {
            get
            {
                var bdes = Enumerable.Empty<BDE>();

                try
                {
                    bdes = BDEService.GetBDEs();
                }
                catch (ConnectionFailedException ex)
                {
                    var dialogService = new DialogService();
                    dialogService.ShowInformationWindow(
                        "Problème de connexion à la base de données !\n" + ex.InnerException.Message,
                        "Connexion impossible !", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                return bdes;
            }
        }

        private BDE _selectedBDE;

        public BDE SelectedBDE
        {
            get => _selectedBDE;
            set
            {
                _selectedBDE = value;
                OnPropertyChanged();
            }
        }

        public string CurrentUser => _loginService.GetLoginContext().User.Name;

        #endregion
    }
}