using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GestionCaisse_MVVM.Model.Entities;

namespace GestionCaisse_MVVM.ViewModel
{
    public class AdministrationViewModel : ViewModelBase
    {
        private List<AdministrationFeature> _features;

        public List<AdministrationFeature> Features
        {
            get { return _features; }
            set { _features = value; }
        }


        public AdministrationViewModel()
        {
            Features = new List<AdministrationFeature>();
            LoadFeatures();
        }

        private void LoadFeatures()
        {
            Features.Add(new AdministrationFeature()
            {
                Name = "Synthèse",
                ImagePath = "/Assets/administration/synthese.png"
            });

            Features.Add(new AdministrationFeature()
            {
                Name = "Historique des ventes",
                ImagePath = "/Assets/administration/history.png"
            });

            Features.Add(new AdministrationFeature()
            {
                Name = "Dûs à chaque BDE",
                ImagePath = "/Assets/administration/accountant.png"
            });

            Features.Add(new AdministrationFeature()
            {
                Name = "Gestion Utilisateurs",
                ImagePath = "/Assets/administration/hierarchical-structure.png"
            });

            Features.Add(new AdministrationFeature()
            {
                Name = "Gestion Produits",
                ImagePath = "/Assets/administration/barcode.png"
            });
        }

    }
}
