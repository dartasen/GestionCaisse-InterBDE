using System.Windows.Controls;
using GestionCaisse_MVVM.ViewModel.AdministrationFeatures;

namespace GestionCaisse_MVVM.View.AdministrationFeatures
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
