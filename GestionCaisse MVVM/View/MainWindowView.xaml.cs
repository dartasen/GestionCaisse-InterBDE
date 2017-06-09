using System.Windows;
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
        }
    }
}