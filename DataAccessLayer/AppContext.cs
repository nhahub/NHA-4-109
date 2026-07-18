using Azure.Messaging;
using DataAccessLayer.Classes;
using DataAccessLayer.Configures;
using DataAccessLayer.ModelContetxt;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
namespace DataAccessLayer
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options)
          : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=DepiProjectDataBase;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserConfigure().Configure(modelBuilder.Entity<User>());
            new AdminConfigure().Configure(modelBuilder.Entity<Admin>());
            new PropertyConfigure().Configure(modelBuilder.Entity<Property>());
            new OwnerConfigure().Configure(modelBuilder.Entity<Owner>());
            new TenantConfigure().Configure(modelBuilder.Entity<Tentant>());
            new ReviewConfigure().Configure(modelBuilder.Entity<Review>());
            new ServiceConfigure().Configure(modelBuilder.Entity<Service>());
            new MessageConfigure().Configure(modelBuilder.Entity<Message>());
            new PreferencesConfiguration().Configure(modelBuilder.Entity<Preferences>());

           
        }



        public DbSet<Message> Messages { get; set; }

        public DbSet<Preferences> Preferences { get; set; }




    }
}