using System;

namespace GestionCaisse_MVVM.ViewModel
{
    public abstract class ViewModelBase
    {
        public Action Close { get; set; }
        public Action Show { get; set; }
        public Action Hide { get; set; }
    }
}
