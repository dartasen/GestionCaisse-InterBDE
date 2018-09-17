using MySql.Data.Entity;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;

namespace GestionCaisse_MVVM
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DBConnection : DbContext
    {
        #region DbSet
        public DbSet<BDE> BDE { get; set; }

        public DbSet<History> History { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Client> Client { get; set; }
        #endregion

        public DBConnection()
          : base(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString)
        {

        }

        public DBConnection(DbConnection existingConnection, bool contextOwnsConnection)
          : base(existingConnection, contextOwnsConnection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BDE>().MapToStoredProcedures();
            modelBuilder.Entity<User>().MapToStoredProcedures();
            modelBuilder.Entity<Client>().MapToStoredProcedures();
            modelBuilder.Entity<Product>().MapToStoredProcedures();
            modelBuilder.Entity<History>().MapToStoredProcedures();
        }
    }
}
