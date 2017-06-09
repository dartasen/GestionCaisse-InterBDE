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
    public class ProductInsertionViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string p = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));

        #region Command
        private ICommand _insertProductToBasket;

        public ICommand InsertProductToBasket
        {
            get { return _insertProductToBasket; }
        }
        #endregion

        #region Properties
        private BasketProduct _SelectedProduct;

        public BasketProduct SelectedProduct
        {
            get { return _SelectedProduct; }
            set
            {
                _SelectedProduct = value;
                OnPropertyChanged();
            }
        }

        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged();
            }          
        }

        public IEnumerable<Product> Products
        {
            get
            {
                IEnumerable<Product> products = Enumerable.Empty<Product>();

                try
                {
                    products = ProductService.GetProducts();
                }
                catch (ConnectionFailedException ex)
                {
                    var dialogService = new DialogService();
                    dialogService.ShowInformationWindow("Problème de connexion à la base de données !\n" + ex.InnerException.Message,
                        "Connexion impossible !", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    Close();
                }

                return products;
            }
        }
        #endregion

        public ProductInsertionViewModel()
        {
            _SelectedProduct = new BasketProduct(null, 1);

            var basketService = BasketService.Instance;

            _insertProductToBasket = new RelayCommand(() =>
           {
               basketService.GetBasket().AddBasketProduct(SelectedProduct);
               Close();
           }, o => true);
        }
    }
}
