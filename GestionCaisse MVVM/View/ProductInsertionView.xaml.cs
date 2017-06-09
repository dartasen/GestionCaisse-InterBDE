using GestionCaisse_MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace GestionCaisse_MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour ProductInsertionView.xaml
    /// </summary>
    public partial class ProductInsertionView : Window
    {
        public ProductInsertionView()
        {
            InitializeComponent();

            var vm = new ProductInsertionViewModel()
            {
                Close = () => this.Close()
            };

            DataContext = vm;

            ProductsListViewBox.Focus();
        }

        /// <summary>
        /// Unselect a product of the ListView if it is out of stock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = sender as ListView;
            if (listView == null) return;

            Product p = listView.SelectedItem as Product;
            if (p == null) return;

            if(p.Quantity == 0)
            {
                listView.UnselectAll();
            }
        }
    }
}
