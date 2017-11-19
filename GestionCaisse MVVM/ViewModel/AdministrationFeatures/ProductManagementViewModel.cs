using System;
using System.Data.Entity;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.ViewModel.AdministrationFeatures
{
    public class ProductManagementViewModel
    {
        public ProductManagementViewModel()
        {
            _products = new CollectionViewSource();
            Refresh();

            SaveChanges = new RelayCommand(() =>
            {
                DialogService dialogService = new DialogService();
                try
                {
                    MessageBoxResult result = dialogService.ShowInformationWindow("Voulez-vous vraiment appliquer ces modifications ?",
                        "Confirmation de l'opération",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);
                    if(result.Equals(MessageBoxResult.Yes))
                        _db.SaveChanges();
                    else
                        return;
                }
                catch (Exception e)
                {
                    dialogService.ShowInformationWindow("Erreur :\n" + e, "Mise à jour impossible !", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
                Refresh();
            }, o => true);

            ResetChanges = new RelayCommand(() => Refresh(), o => true);
        }

        private DBConnection _db;
        public void Refresh()
        {
            _db = new DBConnection();
            _db.Products.Load();
            _products.Source = _db.Products.Local;
        }

        #region Properties
        private CollectionViewSource _products;

        public CollectionViewSource Products
        {
            get => _products;
            set => _products = value;
        }

        #endregion;

        #region Commands
        public ICommand SaveChanges { get; }
        public ICommand ResetChanges { get; }
        #endregion
    }
}
