using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using GestionCaisse_MVVM.Exceptions;

namespace GestionCaisse_MVVM.Model.Services
{
    public class BDEService
    {
        public static IEnumerable<BDE> GetBDEs()
        {
            try
            {
                using (var context = new DBConnection())
                {
                    return new List<BDE>(context.BDE);
                }
            }
            catch (EntityException ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }

        public static Dictionary<string, double> GetBDEsCoefficients()
        {
            try
            {
                var results = new Dictionary<string, double>();
                using (var context = new DBConnection())
                {
                    context.BDE.ToList().ForEach(x => results.Add(x.Nom, x.Taux));
                    return results;
                }
            }
            catch (EntityException ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }

        public static List<BDEDue> GetBDEDues(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                using (var context = new DBConnection())
                {
                    var dues = new List<BDEDue>();
                    List<BDE> bdes = context.BDE.ToList();

                    foreach (BDE bde in bdes)
                    {
                        var query =
                            from history in context.History
                            where history.IdBDEAcheteur == bde.Id && history.DateVente > dateFrom && history.DateVente < dateTo
                            select new PromotionQueryResult()
                            {
                                ProductPrice = context.Product.FirstOrDefault(x => x.IdProduit == history.IdProduit).Prix,
                                Quantity = history.Quantite
                            };

                        dues.Add(new BDEDue()
                        {
                            BDE = bde,
                            Due = ApplyPromotion(query.ToList())
                        });
                    }

                    return dues;
                }
            }
            catch (EntityException ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }

        private class PromotionQueryResult
        {
            public double ProductPrice { get; set; }
            public int Quantity { get; set; }
        }

        public class BDEDue
        {
            private double _due;
            public BDE BDE { get; set; }

            public double Due
            {
                get => _due;
                set => _due = Math.Round(value, 2); //Round the due to 2 decimals
            }

        }

        private static double ApplyPromotion(List<PromotionQueryResult> listToDeducePromotion)
        {
            double promotion = listToDeducePromotion.Where(x => x.ProductPrice >= 0.70).Sum(x => x.Quantity) / 2 * 0.20;

            return listToDeducePromotion.Sum(x => x.Quantity * x.ProductPrice) - promotion;
        }
    }
}