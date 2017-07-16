using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GestionCaisse_MVVM.Model.Services;
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

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Key.Equals(Key.Enter)) return;

            var vm = DataContext as ProductInsertionViewModel;
            if (vm.InsertProductToBasket.CanExecute(null))
                vm.InsertProductToBasket.Execute(null);
        }

        private void ProductInsertionView_OnClosed(object sender, EventArgs e)
        {
            LoginService.Instance.IsTimerActive = true;
        }
    }
}