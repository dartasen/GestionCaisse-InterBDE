using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionCaisse_MVVM.Exceptions;

namespace GestionCaisse_MVVM.Model.Services
{
    public class ClientService
    {
        private static Random random = new Random();

        public static Client CheckPassKey(int passkey)
        {
            try
            {
                using (var context = new DBConnection())
                {
                    try
                    {
                        var client = context.Client.FirstOrDefault(x => x.CodeSecret == passkey);
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
                        context.Client.FirstOrDefault(x => x.IdClient == client.IdClient).Credit -= due;
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

        public static IEnumerable<Client> GetClients()
        {
            try
            {
                using (var context = new DBConnection())
                {
                    try
                    {
                        return context.Client.ToList();
                    }
                    catch (ArgumentNullException)
                    {
                        return new List<Client>();
                    }
                }
            }
            catch (EntityException ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }

        public static int GenerateNewPasskey()
        {
            try
            {
                using (var context = new DBConnection())
                {
                    var newPasskey = random.Next(100000000, 999999999); // creates a 9 digit random no.

                    while (context.Client.Any(x => x.CodeSecret == newPasskey))
                    {
                        newPasskey = random.Next(100000000, 999999999);
                    }

                    return newPasskey;
                }
            }
            catch (EntityException ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }

        public static void RemoveClient(Client client)
        {
            try
            {
                using (var context = new DBConnection())
                {
                    foreach (var history in context.History)
                    {
                        if (history.IdClient == client.IdClient)
                            history.IdClient = null;
                    }

                    context.Client.Remove(context.Client.FirstOrDefault(x => x.IdClient == client.IdClient));

                    context.SaveChanges();
                }
            }
            catch (EntityException ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }

        public static void AddClient(Client client)
        {
            try
            {
                using (var context = new DBConnection())
                {
                    context.Client.Add(client);
                    context.SaveChanges();
                }
            }
            catch (EntityException ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }

        public static void ValidateChanges(Client oldClient, Client newClient)
        {
            try
            {
                using (var context = new DBConnection())
                {
                    if (!oldClient.Nom.Equals(newClient.Nom))
                    {
                        context.Client.FirstOrDefault(x => x.IdClient == oldClient.IdClient).Nom = newClient.Nom;
                    }

                    if (!oldClient.IdCarte.Equals(newClient.IdCarte))
                    {
                        context.Client.FirstOrDefault(x => x.IdClient == oldClient.IdClient).IdCarte = newClient.IdCarte;
                    }

                    if (oldClient.CodeSecret != newClient.CodeSecret)
                    {
                        context.Client.FirstOrDefault(x => x.IdClient == oldClient.IdClient).CodeSecret = newClient.CodeSecret;
                    }

                    if (oldClient.IdBde != newClient.IdBde)
                    {
                        context.Client.FirstOrDefault(x => x.IdClient == oldClient.IdClient).IdBde = newClient.IdBde;
                    }

                    if (oldClient.Credit != newClient.Credit)
                    {
                        context.Client.FirstOrDefault(x => x.IdClient == oldClient.IdClient).Credit = newClient.Credit;
                    }

                    context.SaveChanges();
                }
            }
            catch (EntityException ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }
    }
}
