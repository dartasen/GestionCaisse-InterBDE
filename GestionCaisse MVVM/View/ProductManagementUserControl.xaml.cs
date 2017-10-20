using System.Windows.Controls;
using GestionCaisse_MVVM.ViewModel;

namespace GestionCaisse_MVVM.View
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
