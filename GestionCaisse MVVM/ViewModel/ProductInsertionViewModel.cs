using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GestionCaisse_MVVM.Exceptions;
using GestionCaisse_MVVM.Model.Entities;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.ViewModel
{
    public class ProductInsertionViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string p = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));

        #region Command

        public ICommand InsertProductToBasket { get; }

        #endregion

        #region Properties
        public AutoCompleteFilterPredicate<object> PersonFilter
        {
            get
            {
                return (searchText, obj) =>
                    (obj as Product).Name.ToLower().Contains(searchText.ToLower());
            }
        }

        private BasketProduct _SelectedProduct;

        public BasketProduct SelectedProduct
        {
            get => _SelectedProduct;
            set
            {
                _SelectedProduct = value;
                OnPropertyChanged();
            }
        }

        private int _quantity;

        public int Quantity
        {
            get => _quantity;
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
                var products = Enumerable.Empty<Product>();

                try
                {
                    products = ProductService.GetProducts();
                }
                catch (ConnectionFailedException ex)
                {
                    var dialogService = new DialogService();
                    dialogService.ShowInformationWindow(
                        "Problème de connexion à la base de données !\n" + ex.InnerException.Message,
                        "Connexion impossible !", MessageBoxButton.OK, MessageBoxImage.Error);
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

            InsertProductToBasket = new RelayCommand(() =>
            {
                basketService.GetBasket().AddBasketProduct(SelectedProduct);
                Close();
            }, o => true);
        }      
    }
}