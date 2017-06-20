using System.Windows;
using System.Windows.Controls;
using GestionCaisse_MVVM.ViewModel;

namespace GestionCaisse_MVVM.View
{
    /// <summary>
    ///     Logique d'interaction pour ProductInsertionView.xaml
    /// </summary>
    public partial class ProductInsertionView : Window
    {
        public ProductInsertionView()
        {
            InitializeComponent();

            var vm = new ProductInsertionViewModel
            {
                Close = () => Close()
            };

            DataContext = vm;

            AutoCompleteBox.Focus();
        }

        /// <summary>
        ///     Unselect a product of the ListView if it is out of stock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = sender as ListView;
            if (listView == null) return;

            var p = listView.SelectedItem as Product;
            if (p == null) return;

            if (p.Quantity == 0)
                listView.UnselectAll();
        }

        private void AutoCompleteBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var autocompleteBox = sender as AutoCompleteBox;
            if (autocompleteBox == null) return;

            var selectedItem = autocompleteBox.SelectedItem as Product;
            if (selectedItem == null) return;

            if (selectedItem.Quantity == 0)
                autocompleteBox.SelectedItem = null;
        }
    }
}