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

        public double SnacksPrice => _products.Where(x => x.Product.Categorie.Equals("snack"))
            .Sum(x => x.Quantite * x.Product.Prix);

        public string SnacksPriceFormated => SnacksPrice + " €";

        public double DrinksPrice => _products.Where(x => x.Product.Categorie.Equals("boisson"))
            .Sum(x => x.Quantite * x.Product.Prix);

        public string DrinksPriceFormated => DrinksPrice + " €";

        public double TotalPrice
        {
            get
            {
                var promotion = _products.Where(x => x.Product.Prix >= 0.70).Sum(x => x.Quantite) / 2 * 0.20;

                return _products.Sum(x => x.Quantite * x.Product.Prix) - promotion;
            }
        }

        public string TotalPriceFormated => TotalPrice + " €";

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string p = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }

        public void AddBasketProduct(BasketProduct basketProductToAdd)
        {
            if (basketProductToAdd.Product == null) throw new IllegalProductInsertion("Vous devez sélectionner un produit avant de l'ajouter !");

            var idProduct = basketProductToAdd.Product.IdProduit;
            var quantityToAdd = basketProductToAdd.Quantite;

            if (quantityToAdd <= 0) throw new IllegalProductInsertion("La quantité doit-être positive !");
            if (quantityToAdd > basketProductToAdd.Product.Quantite)
                throw new IllegalProductInsertion("La quantité ne peut pas être suppérieure au stock !\n" +
                          $"Vous demandez {quantityToAdd} {basketProductToAdd.Product.Nom} alors qu'il n'en reste que {basketProductToAdd.Product.Quantite}.");

            //If the product is already on the basket
            if (_products.Select(x => x.Product.IdProduit).Contains(idProduct))
            {
                foreach (BasketProduct t in _products)
                {
                    if (t.Product.IdProduit == idProduct)
                    {
                        if ((t.Quantite + 1) <= basketProductToAdd.Product.Quantite)
                        {
                            t.Quantite += quantityToAdd;
                        }
                        else
                        {
                            throw new IllegalProductInsertion("La quantité ne peut pas être suppérieure au stock !\n" +
                            $"Vous demandez {basketProductToAdd.Product.Quantite + 1} {basketProductToAdd.Product.Nom} alors qu'il n'en reste que {basketProductToAdd.Product.Quantite}.");
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
            if (basketProduct.Quantite + 1 > basketProduct.Product.Quantite) return;
            basketProduct.Quantite++;
            NotifyProperties();
        }

        public void DecreaseQuantity(BasketProduct basketProduct)
        {
            if (basketProduct.Quantite -1 < 0) return;
            basketProduct.Quantite--;
            NotifyProperties();
        }

        public void ResetBasket()
        {
            _products.Clear();
            NotifyProperties();
        }

        private void NotifyProperties()
        {
            OnPropertyChanged(nameof(SnacksPriceFormated));
            OnPropertyChanged(nameof(DrinksPriceFormated));
            OnPropertyChanged(nameof(TotalPriceFormated));
        }
    }
}