using GestionCaisse_MVVM.Model.Entities;
using System;

namespace GestionCaisse_MVVM.Model.Events
{
    public class BasketProductAddedEventArgs : EventArgs
    {
        private readonly BasketProduct _basketProductAdded;
        public BasketProduct BasketProductAdded => _basketProductAdded;

        public BasketProductAddedEventArgs(BasketProduct basketProductAdded)
        {
            _basketProductAdded = basketProductAdded;
        }
    }
}
