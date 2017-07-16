using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.ViewModel
{
    public class ProductManagementViewModel
    {
        public ProductManagementViewModel()
        {
            _products = ProductService.GetProducts();
        }

        #region Properties
        private IEnumerable<Product> _products;

        public IEnumerable<Product> Products
        {
            get { return _products; }
            set { _products = value; }
        }

        #endregion
    }
}
