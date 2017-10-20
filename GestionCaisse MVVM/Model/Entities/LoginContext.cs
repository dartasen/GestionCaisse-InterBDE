using System.Collections.Generic;

namespace GestionCaisse_MVVM.Model.Entities
{
    public class LoginContext
    {
        public User User { get; set; }
        public BDE BuyingBDE { get; set; }
        public List<int> IdSellsMade { get; set; }
    }
}