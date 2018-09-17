using System;
using System.Data.Entity.Core;
using System.Linq;
using GestionCaisse_MVVM.Exceptions;
using GestionCaisse_MVVM.Model.Entities;

namespace GestionCaisse_MVVM.Model.Services
{
    public class BasketService
    {
        private readonly Basket _basket = new Basket();

        public Basket GetBasket()
        {
            return _basket;
        }

        /// <summary>
        ///     Register the sell of products to the DB
        /// </summary>
        public bool ValidateSell(int? clientId = null)
        {
            if (!GetBasket().Products.Any()) return false;

            var loginService = LoginService.Instance.GetLoginContext();

            try
            {
                using (var context = new DBConnection())
                {
                    foreach (var bp in GetBasket().Products)
                    {
                        context.History.Add(new History
                        {
                            IdUtilisateur = loginService.User.IdUtilisateur,
                            IdProduit = bp.Product.IdProduit,
                            Quantite = bp.Quantite,
                            IdBDEAcheteur = loginService.BuyingBDE.Id,
                            IdClient = clientId,
                            DateVente = DateTime.Now
                        });

                        context.Product.FirstOrDefault(x => x.IdProduit == bp.Product.IdProduit).Quantite -= bp.Quantite;
                    }

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }

        #region Singleton

        private static BasketService _instance;

        public static BasketService Instance => _instance ?? (_instance = new BasketService());

        private BasketService()
        {
        }

        #endregion
    }
}