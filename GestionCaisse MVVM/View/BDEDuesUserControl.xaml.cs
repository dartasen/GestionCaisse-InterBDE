using System.Windows.Controls;
using GestionCaisse_MVVM.ViewModel;

namespace GestionCaisse_MVVM.View
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
