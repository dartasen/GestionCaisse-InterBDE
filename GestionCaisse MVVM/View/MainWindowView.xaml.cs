using GestionCaisse_MVVM.ViewModel;
using System.Windows;

namespace GestionCaisse_MVVM
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();

            var vm = new MainWindowViewModel()
            {
                Close = () => this.Close()
            };

            DataContext = vm;
        }
    }
}
