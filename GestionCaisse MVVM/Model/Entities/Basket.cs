using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GestionCaisse_MVVM.Model.Entities
{
    public class Basket : INotifyPropertyChanged
    {
        private readonly ObservableCollection<BasketProduct> _products;

        public Basket()
        {
            _products = new ObservableCollection<BasketProduct>();
        }

        public IEnumerable<BasketProduct> Products => _products;

        public string SnacksPrice
        {
            get
            {
                return _products.Where(x => x.Product.Category.Equals("snack")).Sum(x => x.Quantity * x.Product.Price) +
                       " €";
            }
        }

        public string DrinksPrice
        {
            get
            {
                return _products.Where(x => x.Product.Category.Equals("boisson"))
                           .Sum(x => x.Quantity * x.Product.Price) + " €";
            }
        }

        public string TotalPrice
        {
            get { return _products.Sum(x => x.Quantity * x.Product.Price) + " €"; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string p = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }

        public void AddBasketProduct(BasketProduct basketProductToAdd)
        {
            if (basketProductToAdd.Product == null) return;
            if (basketProductToAdd.Quantity <= 0) return;
            if (basketProductToAdd.Quantity > basketProductToAdd.Product.Quantity) return;

            _products.Add(basketProductToAdd);
            NotifyProperties();

            //TODO Add exceptions
        }

        public void ResetBasket()
        {
            _products.Clear();
            NotifyProperties();
        }

        private void NotifyProperties()
        {
            OnPropertyChanged(nameof(SnacksPrice));
            OnPropertyChanged(nameof(DrinksPrice));
            OnPropertyChanged(nameof(TotalPrice));
        }
    }
}