using GestionCaisse_MVVM.Exceptions;
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

        public void AddBasketProduct(BasketProduct basketProductToAdd)
        {
            if (basketProductToAdd.Product == null) throw new IllegalProductInsertion("Vous devez sélectionner un produit avant de l'ajouter !");

            var idProduct = basketProductToAdd.Product.IDProduct;
            var quantityToAdd = basketProductToAdd.Quantity;

            if (quantityToAdd <= 0) throw new IllegalProductInsertion("La quantité doit-être positive !");
            if (quantityToAdd > basketProductToAdd.Product.Quantity)
                throw new IllegalProductInsertion("La quantité ne peut pas être suppérieure au stock !\n" +
                          $"Vous demandez {quantityToAdd} {basketProductToAdd.Product.Name} alors qu'il n'en reste que {basketProductToAdd.Product.Quantity}.");

            //If the product is already on the basket
            if (_products.Select(x => x.Product.IDProduct).Contains(idProduct))
            {
                foreach (BasketProduct t in _products)
                {
                    if (t.Product.IDProduct == idProduct)
                    {
                        if ((t.Quantity + 1) <= basketProductToAdd.Product.Quantity)
                        {
                            t.Quantity += quantityToAdd;
                        }
                        else
                        {
                            throw new IllegalProductInsertion("La quantité ne peut pas être suppérieure au stock !\n" +
                            $"Vous demandez {basketProductToAdd.Product.Quantity + 1} {basketProductToAdd.Product.Name} alors qu'il n'en reste que {basketProductToAdd.Product.Quantity}.");
                        }
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

        public void IncreaseQuantity(BasketProduct basketProduct)
        {
            if (basketProduct.Quantity + 1 > basketProduct.Product.Quantity) return;
            basketProduct.Quantity++;
            NotifyProperties();
        }

        public void DecreaseQuantity(BasketProduct basketProduct)
        {
            if (basketProduct.Quantity -1 < 0) return;
            basketProduct.Quantity--;
            NotifyProperties();
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