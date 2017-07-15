using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GestionCaisse_MVVM.Model.Entities
{
    public class AdministrationFeature
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public UserControl UserControl { get; set; }
    }
}
