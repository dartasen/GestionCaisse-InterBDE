using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using GestionCaisse_MVVM.Model.Services;
using GestionCaisse_MVVM.ViewModel;

namespace GestionCaisse_MVVM.View
{
    /// <summary>
    ///     Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();

            var vm = new MainWindowViewModel
            {
                Close = () => Close()
            };

            DataContext = vm;

            Test();
        }

        public void Test()
        {
            var vm = DataContext as MainWindowViewModel;
            if (vm == null) return;
            string bde = LoginService.Instance.GetLoginContext().BuyingBDE.Name;
            BrushConverter bc = new BrushConverter();
            switch (bde)
            {
                case "Informatique":
                    Background = (Brush)bc.ConvertFrom("#D3D3D3");
                    break;
                case "Biologie":
                    Background = (Brush)bc.ConvertFrom("#74FFA6"); 
                    break; 
                case "RT":
                    Background = (Brush)bc.ConvertFrom("#FFA8E0"); 
                    break;
                default:
                    Background = Brushes.White;
                    break;
            }
        }

        private void MainWindowView_OnLoaded(object sender, RoutedEventArgs e)
        {
            // Set buying BDE to default buying BDE : user's own BDE
            BDE currentBDE = LoginService.Instance.GetLoginContext().BuyingBDE;

            for (int i = 0; i < ComboBox.Items.Count; i++)
            {
                if (((BDE)ComboBox.Items[i]).idBDE == currentBDE.idBDE) ComboBox.SelectedIndex = i;
            }
        }

        private void UIElement_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Delete))
                ((MainWindowViewModel)DataContext).DeleteBasketProduct.Execute(null);
        }

        private void MainWindowView_OnClosed(object sender, EventArgs e)
        {
            ((MainWindowViewModel)DataContext).Logout.Execute(null);
        }
    }
}