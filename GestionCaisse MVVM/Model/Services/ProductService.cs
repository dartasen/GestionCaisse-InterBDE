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
                    return context.Product.OrderBy(x => x.Nom).ToList();
                }
            }
            catch (EntityException ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }

        //TODO Amméliorer comparaison date => faire sur année, puis mois puis jour
        public static List<QueryHistory> GetHistory(DateTime dateFrom, DateTime dateTo, int? idUser = null)
        {
            try
            {
                using (var context = new DBConnection())
                {
                    var query =
                         from history in context.History
                         join user in context.User on history.IdUtilisateur equals user.IdUtilisateur
                         join product in context.Product on history.IdProduit equals product.IdProduit
                         join buyingBDE in context.BDE on history.IdBDEAcheteur equals buyingBDE.Id
                         where (idUser == null && history.DateVente >= dateFrom && history.DateVente <= dateTo) || (idUser != null && history.DateVente >= dateFrom && history.DateVente <= dateTo && user.IdUtilisateur == idUser)
                         select new QueryHistory
                         {
                             IdVente = history.IdVente,
                             Username = user.Nom,
                             ProductName = product.Nom,
                             Quantite = history.Quantite,
                             IdClient = history.IdClient,
                             BuyingBDEName = buyingBDE.Nom,
                             DateVente = history.DateVente
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
                    var sellToDelete = context.History.FirstOrDefault(x => x.IdVente == idSale);
                    if (sellToDelete == null) return false;

                    context.Product.FirstOrDefault(x => x.IdProduit == sellToDelete.IdProduit).Quantite += sellToDelete.Quantite;

                    if (sellToDelete.IdClient != null)
                        context.Client.FirstOrDefault(x => x.IdClient == sellToDelete.IdClient).Credit +=
                            context.Product.FirstOrDefault(x => x.IdProduit == sellToDelete.IdProduit).Prix * sellToDelete.Quantite;

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
    }
}