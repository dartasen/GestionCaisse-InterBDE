using System;
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
                    return context.Products.OrderBy(x => x.Name).ToList();
                }
            }
            catch (EntityException ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }

        //TODO Amméliorer comparaison date => faire sur année, puis mois puis jour
        public static List<HistoryQueryResult> GetHistory(DateTime dateFrom, DateTime dateTo, int? idUser = null)
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
                        where (idUser == null && history.SaleDate >= dateFrom && history.SaleDate <= dateTo) ||
                              (idUser != null && history.SaleDate >= dateFrom && history.SaleDate <= dateTo && user.IdUser == idUser)
                        select new HistoryQueryResult()
                        {
                            IdSale = history.IdSale,
                            Username = user.Name,
                            ProductName = product.Name,
                            Quantity = history.Quantity,
                            IdClient = history.IdClient,
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

        public static bool RollBackSell(int idSale)
        {
            try
            {
                using (var context = new DBConnection())
                {
                    var sellToDelete = context.History.FirstOrDefault(x => x.IdSale == idSale);
                    if (sellToDelete == null) return false;

                    context.Products.FirstOrDefault(x => x.IDProduct == sellToDelete.IdProduct).Quantity += sellToDelete.Quantity;

                    if (sellToDelete.IdClient != null)
                        context.Clients.FirstOrDefault(x => x.IdClient == sellToDelete.IdClient).Balance +=
                            context.Products.FirstOrDefault(x => x.IDProduct == sellToDelete.IdProduct).Price;

                    context.History.Remove(sellToDelete);
                    context.SaveChanges();
                    return true;
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