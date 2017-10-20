using System.Windows;
using GestionCaisse_MVVM.ViewModel;

namespace GestionCaisse_MVVM.View
{
    /// <summary>
    /// Interaction logic for AddUserView.xaml
    /// </summary>
    public partial class AddUserView : Window
    {
        public AddUserView()
        {
            InitializeComponent();

            DataContext = new AddUserViewModel()
            {
                Close = () => Close()
            };

            LoginTextBlock.Focus();
        }
    }
}
