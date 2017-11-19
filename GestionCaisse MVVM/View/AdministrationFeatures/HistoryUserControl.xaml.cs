using System.Windows.Controls;
using GestionCaisse_MVVM.ViewModel.AdministrationFeatures;

namespace GestionCaisse_MVVM.View.AdministrationFeatures
{
    /// <summary>
    /// Interaction logic for HistoryUserControl.xaml
    /// </summary>
    public partial class HistoryUserControl : UserControl
    {
        public HistoryUserControl()
        {
            InitializeComponent();
            DataContext = new HistoryViewModel();
        }
    }
}
