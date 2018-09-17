using System;
using System.Windows.Controls;
using System.Windows.Input;
using GestionCaisse_MVVM.Model.Services;
using GestionCaisse_MVVM.ViewModel;
using MahApps.Metro.Controls;

namespace GestionCaisse_MVVM.View
{
    /// <summary>
    ///     Logique d'interaction pour ProductInsertionView.xaml
    /// </summary>
    public partial class ProductInsertionView : MetroWindow
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
            if (!(sender is ListView listView)) return;

            if (!(listView.SelectedItem is Product p)) return;

            if (p.Quantite == 0)
                listView.UnselectAll();
        }

        private void AutoCompleteBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(sender is AutoCompleteBox autocompleteBox)) return;

            if (!(autocompleteBox.SelectedItem is Product selectedItem)) return;

            if (selectedItem.Quantite == 0)
                autocompleteBox.SelectedItem = null;
        }

        // Enabling pressing "Enter" key instead of hitting the button
        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Key.Equals(Key.Enter)) return;

            var vm = DataContext as ProductInsertionViewModel;
            if (vm.InsertProductToBasket.CanExecute(null))
                vm.InsertProductToBasket.Execute(null);
        }

        // Re-activate the timer when closing
        private void ProductInsertionView_OnClosed(object sender, EventArgs e)
        {
            LoginService.Instance.IsTimerActive = true;
        }

        /// <summary>
        ///     Enable double-clicking instead of hitting validation button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is ProductInsertionViewModel vm) vm.InsertProductToBasket.Execute(vm.SelectedProduct);
        }
    }
}