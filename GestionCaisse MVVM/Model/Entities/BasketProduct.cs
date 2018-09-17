using System.ComponentModel;
using System.Runtime.CompilerServices;
using GestionCaisse_MVVM.Annotations;

namespace GestionCaisse_MVVM.Model.Entities
{
    public class BasketProduct : INotifyPropertyChanged
    {
        private Product _product;
        private int _quantite;

        public BasketProduct(Product product, int quantity)
        {
            Product = product;
            Quantite = quantity;
        }

        public Product Product
        {
            get => _product;
            set
            { _product = value; OnPropertyChanged(); }
        }
        
        public int Quantite
        {
            get => _quantite;
            set { _quantite = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}