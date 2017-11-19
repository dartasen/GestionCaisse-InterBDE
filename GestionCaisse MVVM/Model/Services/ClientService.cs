using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionCaisse_MVVM.Exceptions;

namespace GestionCaisse_MVVM.Model.Services
{
    public class ClientService
    {
        public static Client CheckPassKey(int passkey)
        {
            try
            {
                using (var context = new DBConnection())
                {
                    try
                    {
                        Client client = context.Clients.FirstOrDefault(x => x.Passkey == passkey);
                        return client;
                    }
                    catch (ArgumentNullException)
                    {
                        return null;
                    }
                }
            }
            catch (EntityException ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }

        public static void ChargeClient(Client client, double due)
        {
            try
            {
                using (var context = new DBConnection())
                {
                    try
                    {
                        context.Clients.FirstOrDefault(x => x.IdClient == client.IdClient).Balance -= due;
                        context.SaveChanges();
                    }
                    catch (ArgumentNullException) { }
                }
            }
            catch (EntityException ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }
    }
}
