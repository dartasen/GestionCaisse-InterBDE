using GestionCaisse_MVVM.Exceptions;
using GestionCaisse_MVVM.Model.Entities;
using GestionCaisse_MVVM.Model.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace GestionCaisse_MVVM.ViewModel
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string p = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));

        private BasketService _basketService = BasketService.Instance;
        private LoginService _loginService = LoginService.Instance;

        #region Commandes
        private ICommand _resetBasket;

        public ICommand ResetBasket
        {
            get { return _resetBasket; }
        }

        private ICommand _insertProduct;

        public ICommand InsertProduct
        {
            get { return _insertProduct; }
        }

        private ICommand _validateSell;

        public ICommand ValidateSell
        {
            get { return _validateSell; }
        }

        private ICommand _logout;

        public ICommand Logout
        {
            get { return _logout; }
        }

        #endregion

        #region Properties
        public Basket Basket
        {
            get { return BasketService.Instance.GetBasket(); }
        }

        public IEnumerable<BDE> BDEs
        {
            get
            {
                IEnumerable<BDE> bdes = Enumerable.Empty<BDE>();

                try
                {
                    bdes = BDEService.GetBDEs();
                }
                catch (ConnectionFailedException ex)
                {
                    var dialogService = new DialogService();
                    dialogService.ShowInformationWindow("Problème de connexion à la base de données !\n" + ex.InnerException.Message,
                        "Connexion impossible !", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }

                return bdes;
            }
        }

        private BDE _selectedBDE;
        public BDE SelectedBDE
        {
            get { return _selectedBDE; }
            set { _selectedBDE = value; OnPropertyChanged(); }
        }

        public string CurrentUser
        {
            get
            {
                return _loginService.GetLoginContext().User.Name;
            }
        }
        #endregion

        public MainWindowViewModel()
        {
            var dialogService = new DialogService();

            _insertProduct = new RelayCommand(() => dialogService.ShowProductInsertPage(), o => true);
            _resetBasket = new RelayCommand(() => _basketService.GetBasket().ResetBasket(), o => true);
            _validateSell = new RelayCommand(() =>
            {
                try
                {
                    if (_basketService.ValidateSell()) 
                    {
                        _basketService.GetBasket().ResetBasket();
                        dialogService.ShowInformationWindow(
                            "Vente effectuée !",
                            "Confirmation",
                            System.Windows.MessageBoxButton.OK,
                            System.Windows.MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        dialogService.ShowInformationWindow(
                            "Vente invalide ou impossible !",
                            "Attention",
                            System.Windows.MessageBoxButton.OK,
                            System.Windows.MessageBoxImage.Hand);
                    }
                }
                catch (ConnectionFailedException ex)
                {
                    dialogService.ShowInformationWindow("Problème de connexion à la base de données !\n" + ex.InnerException.Message,
                        "Connexion impossible !", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
            }, o => true);

            _logout = new RelayCommand(() =>
            {
                _basketService.GetBasket().ResetBasket();
                Close();
            }, o => true);

            //TODO Sélectionner le BDE de l'utilisateur par défaut
            //WORKAROUND
            SelectedBDE = LoginService.Instance.GetLoginContext().BuyingBDE;
        }
    }
}
