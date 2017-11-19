using System.Windows.Controls;
using GestionCaisse_MVVM.ViewModel.AdministrationFeatures;

namespace GestionCaisse_MVVM.View.AdministrationFeatures
{
    /// <summary>
    /// Interaction logic for BDEDuesUserControl.xaml
    /// </summary>
    public partial class BDEDuesUserControl : UserControl
    {
        public BDEDuesUserControl()
        {
            InitializeComponent();
            DataContext = new BDEDuesViewModel();
        }
    }
}
