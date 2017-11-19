using System.Windows;
using GestionCaisse_MVVM.ViewModel.ClientCashing;

namespace GestionCaisse_MVVM.View.ClientCashing
{
    /// <summary>
    /// Interaction logic for ClientCashing.xaml
    /// </summary>
    public partial class ClientCashing : Window
    {
        public ClientCashing()
        {
            InitializeComponent();

            DataContext = new ClientCashingViewModel()
            {
                Close = () => Close()
            };
        }
    }
}
