using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GestionCaisse_MVVM.Model.Entities
{
    public class Basket : INotifyPropertyChanged
    {
        //TODO Gérer les changements de quantité dans l'UI
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
                return _products.Where(x => x.Product.Category.Equals("snack"))
                    .Sum(x => x.Quantity * x.Product.Price) + " €";
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
            get
            {
                double promotion = _products.Where(x => x.Product.Price >= 0.70).Sum(x => x.Quantity) / 2 * 0.20;

                return _products.Sum(x => x.Quantity * x.Product.Price) - promotion + " €";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string p = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }

        //TODO Ajouter exceptions quand l'ajout n'est pas correcte
        public void AddBasketProduct(BasketProduct basketProductToAdd)
        {
            if (basketProductToAdd.Product == null) return;

            var idProduct = basketProductToAdd.Product.IDProduct;
            var quantityToAdd = basketProductToAdd.Quantity;

            if (quantityToAdd <= 0) return;
            if (quantityToAdd > basketProductToAdd.Product.Quantity) return;

            //If the product is already on the basket
            if (_products.Select(x => x.Product.IDProduct).Contains(idProduct))
            {
                foreach (BasketProduct t in _products)
                {
                    if (t.Product.IDProduct == idProduct)
                    {
                        t.Quantity += quantityToAdd;
                    }
                }
            }
            else
            {
                _products.Add(basketProductToAdd);
            }

            NotifyProperties();
        }

        public void RemoveProduct(BasketProduct basketProduct)
        {
            if (basketProduct == null) return;
            _products.Remove(basketProduct);
            NotifyProperties();
        }

        public void UpdateQuantity(Product product, int quantity)
        {
            
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