using System.ComponentModel;
using System.Runtime.CompilerServices;
using GestionCaisse_MVVM.Annotations;

namespace GestionCaisse_MVVM.Model.Entities
{
    public class BasketProduct : INotifyPropertyChanged
    {
        private Product _product;
        private int _quantity;

        public BasketProduct(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public Product Product
        {
            get { return _product; }
            set
            { _product = value; OnPropertyChanged(); }
        }
        
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}