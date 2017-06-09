using GestionCaisse_MVVM.Exceptions;
using System.Collections.Generic;
using System.Data.Entity.Core;

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
                    return new List<BDE>(context.BDEs);
                }
            }
            catch (EntityException ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }
    }
}
