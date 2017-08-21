using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.ViewModel
{
    public class ProductManagementViewModel
    {
        public ProductManagementViewModel()
        {
            _products = new CollectionViewSource();
            Refresh();

            SaveChanges = new RelayCommand(() =>
            {
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    DialogService dialogService = new DialogService();
                    dialogService.ShowInformationWindow("Erreur :\n" + e.ToString(), "Mise à jour impossible !", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
                finally { Refresh(); }
            }, o => true);

            ResetChanges = new RelayCommand(() => Refresh(), o => true);
        }

        private DBConnection db;
        public void Refresh()
        {
            db = new DBConnection();
            db.Products.Load();
            _products.Source = db.Products.Local;
        }

        #region Properties
        private CollectionViewSource _products;

        public CollectionViewSource Products
        {
            get { return _products; }
            set { _products = value; }
        }

        #endregion;

        #region Commands
        public ICommand SaveChanges { get; }
        public ICommand ResetChanges { get; }
        #endregion
    }
}
