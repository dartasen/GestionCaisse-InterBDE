using System.Windows.Controls;
using GestionCaisse_MVVM.ViewModel;

namespace GestionCaisse_MVVM.View
{
    /// <summary>
    /// Interaction logic for QuickCalcsUserControl.xaml
    /// </summary>
    public partial class QuickCalcsUserControl : UserControl
    {
        public QuickCalcsUserControl()
        {
            InitializeComponent();

            DataContext = new QuickCalcsViewModel();
        }
    }
}
