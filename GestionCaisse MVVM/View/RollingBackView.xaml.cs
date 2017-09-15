using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GestionCaisse_MVVM.ViewModel;

namespace GestionCaisse_MVVM.View
{
    /// <summary>
    /// Interaction logic for RollingBackView.xaml
    /// </summary>
    public partial class RollingBackView : Window
    {
        public RollingBackView()
        {
            InitializeComponent();

            var vm = new RollingBackViewModel
            {
                Close = () => Close(),
                Hide = () => Hide()
            };

            DataContext = vm;
        }
    }
}
