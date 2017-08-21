using System.Collections.Generic;
using GestionCaisse_MVVM.Model.Entities;
using GestionCaisse_MVVM.Model.Services;
using GestionCaisse_MVVM.View;

namespace GestionCaisse_MVVM.ViewModel
{
    public class AdministrationViewModel : ViewModelBase
    {
        public AdministrationViewModel()
        {
            Features = new List<AdministrationFeature>();
            LoadFeatures();
            SelectedAdministrationFeature = Features[1];
        }

        private void LoadFeatures()
        {
            Features.Add(new AdministrationFeature()
            {
                Name = "Synthèse",
                ImagePath = "/Assets/administration/synthesis.png",
                UserControl = new SynthesisUserControl()
            });

            Features.Add(new AdministrationFeature()
            {
                Name = "Historique des ventes",
                ImagePath = "/Assets/administration/history.png",
                UserControl = new HistoryUserControl()
            });

            Features.Add(new AdministrationFeature()
            {
                Name = "Dûs de chaque BDE",
                ImagePath = "/Assets/administration/accountant.png",
                UserControl = new BDEDuesUserControl()
            });

            Features.Add(new AdministrationFeature()
            {
                Name = "Gestion Utilisateurs",
                ImagePath = "/Assets/administration/hierarchical-structure.png",
                UserControl = new UserManagementUserControl()
            });

            Features.Add(new AdministrationFeature()
            {
                Name = "Gestion Produits",
                ImagePath = "/Assets/administration/barcode.png",
                UserControl = new ProductManagementUserControl()
            });
        }

        #region Properties
        private AdministrationFeature _selectedAdministrationFeature;

        public AdministrationFeature SelectedAdministrationFeature
        {
            get => _selectedAdministrationFeature;
            set => _selectedAdministrationFeature = value;
        }

        private List<AdministrationFeature> _features;

        public List<AdministrationFeature> Features
        {
            get => _features;
            set => _features = value;
        }
        #endregion
    }
}
