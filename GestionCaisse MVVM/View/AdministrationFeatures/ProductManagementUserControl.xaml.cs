using System.Windows.Controls;
using GestionCaisse_MVVM.ViewModel.AdministrationFeatures;

namespace GestionCaisse_MVVM.View.AdministrationFeatures
{
    /// <summary>
    /// Interaction logic for ProductManagementUserControl.xaml
    /// </summary>
    public partial class ProductManagementUserControl : UserControl
    {
        public ProductManagementUserControl()
        {
            InitializeComponent();
            DataContext = new ProductManagementViewModel();
        }
    }
}
