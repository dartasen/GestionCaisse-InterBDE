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
                        var client = context.Clients.FirstOrDefault(x => x.Passkey == passkey);
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

        public static IEnumerable<Client> GetClients()
        {
            try
            {
                using (var context = new DBConnection())
                {
                    try
                    {
                        return context.Clients.ToList();
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

                    while (context.Clients.Any(x => x.Passkey == newPasskey))
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

                    context.Clients.Remove(context.Clients.FirstOrDefault(x => x.IdClient == client.IdClient));

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
                    context.Clients.Add(client);
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
                    if (!oldClient.Name.Equals(newClient.Name))
                    {
                        context.Clients.FirstOrDefault(x => x.IdClient == oldClient.IdClient).Name = newClient.Name;
                    }

                    if (!oldClient.BadgeID.Equals(newClient.BadgeID))
                    {
                        context.Clients.FirstOrDefault(x => x.IdClient == oldClient.IdClient).BadgeID = newClient.BadgeID;
                    }

                    if (oldClient.Passkey != newClient.Passkey)
                    {
                        context.Clients.FirstOrDefault(x => x.IdClient == oldClient.IdClient).Passkey = newClient.Passkey;
                    }

                    if (oldClient.IdBDE != newClient.IdBDE)
                    {
                        context.Clients.FirstOrDefault(x => x.IdClient == oldClient.IdClient).IdBDE = newClient.IdBDE;
                    }

                    if (oldClient.Balance != newClient.Balance)
                    {
                        context.Clients.FirstOrDefault(x => x.IdClient == oldClient.IdClient).Balance = newClient.Balance;
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
