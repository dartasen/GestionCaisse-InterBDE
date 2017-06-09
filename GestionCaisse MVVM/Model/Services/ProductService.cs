using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using GestionCaisse_MVVM.Exceptions;

namespace GestionCaisse_MVVM.Model.Services
{
    public class ProductService
    {
        public static IEnumerable<Product> GetProducts()
        {
            try
            {
                using (var context = new DBConnection())
                {
                    return new List<Product>(context.Products.OrderBy(x => x.Category).ThenBy(x => x.Name).ToList());
                }
            }
            catch (EntityException ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }
    }
}