using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
                    return context.Products.OrderBy(x => x.Category).ThenBy(x => x.Name).ToList();
                }
            }
            catch (EntityException ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }

        public static List<HistoryQueryResult> GetHistory(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                using (var context = new DBConnection())
                {
                    var query = 
                        from history in context.History
                        join user in context.Users on history.IdUser equals user.IdUser
                        join product in context.Products on history.IdProduct equals product.IDProduct
                        join buyingBDE in context.BDEs on history.IdBuyingBDE equals buyingBDE.idBDE
                        where history.SaleDate > dateFrom && history.SaleDate < dateTo
                        select new HistoryQueryResult(){
                            IdSale = history.IdSale,
                            Username = user.Name,
                            ProductName = product.Name,
                            Quantity = history.Quantity,
                            BuyingBDEName = buyingBDE.Name,
                            SaleDate = history.SaleDate
                        };

                    return query.ToList();
                }
            }
            catch (EntityException ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }

        public class HistoryQueryResult : History
        {
            public string Username { get; set; }
            public string ProductName { get; set; }
            public string BuyingBDEName { get; set; }
            public string FormatedSaleDate => SaleDate.ToString("dd/MM/yyyy HH:MM:ss");
        }
    }
}