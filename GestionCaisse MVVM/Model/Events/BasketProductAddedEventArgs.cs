using System;
using GestionCaisse_MVVM.Model.Entities;

namespace GestionCaisse_MVVM.Model.Events
{
    public class BasketProductAddedEventArgs : EventArgs
    {
        public BasketProductAddedEventArgs(BasketProduct basketProductAdded)
        {
            BasketProductAdded = basketProductAdded;
        }

        public BasketProduct BasketProductAdded { get; }
    }
}