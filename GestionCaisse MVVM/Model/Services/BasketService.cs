using GestionCaisse_MVVM.Exceptions;
using GestionCaisse_MVVM.Model.Entities;
using System;
using System.Data.Entity.Core;
using System.Linq;

namespace GestionCaisse_MVVM.Model.Services
{
    public class BasketService
    {
        #region Singleton
        private static BasketService _instance;
        public static BasketService Instance
        {
            get
            {
                if (_instance == null) _instance = new BasketService();
                return _instance;
            }
        }

        private BasketService() { }
        #endregion

        private Basket _basket = new Basket();

        public Basket GetBasket()
        {
            return _basket;
        }
        
        /// <summary>
        /// Register the sell of products to the DB
        /// </summary>
        public bool ValidateSell()
        {
            if (!GetBasket().Products.Any()) return false;

            var loginService = LoginService.Instance.GetLoginContext();

            try
            {
                using (DBConnection context = new DBConnection())
                {
                    foreach (BasketProduct bp in GetBasket().Products)
                    {
                        context.History.Add(new History()
                        {
                            IdUser = loginService.User.IdUser,
                            IdProduct = bp.Product.IDProduct,
                            Quantity = bp.Quantity,
                            IdBuyingBDE = loginService.BuyingBDE.idBDE,
                            SaleDate = DateTime.Now
                        });

                        context.Products.Where(x => x.IDProduct == bp.Product.IDProduct).FirstOrDefault().Quantity--;
                    }
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
