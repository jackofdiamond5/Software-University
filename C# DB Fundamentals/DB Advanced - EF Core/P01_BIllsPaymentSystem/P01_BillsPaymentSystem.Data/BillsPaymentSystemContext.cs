using System.IO;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using P01_BillsPaymentSystem.Data.Models;
using P01_BillsPaymentSystem.Data.Models.EntityConfig;

namespace P01_BillsPaymentSystem.Data
{
    public class BillsPaymentSystemContext : DbContext
    {
        public BillsPaymentSystemContext() { }

        public BillsPaymentSystemContext(DbContextOptions options)
            : base(options) { }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<CreditCard> CreditCards { get; set; }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            var settings = File.ReadAllText("settings.config");
            dynamic settingsParsed = JsonConvert.DeserializeObject(settings);

            string connectionString = settingsParsed.ConnectionString.ToString();

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BankAccountConfig());

            modelBuilder.ApplyConfiguration(new CreditCardConfig());

            modelBuilder.ApplyConfiguration(new PaymentMethodConfig());

            modelBuilder.ApplyConfiguration(new UserConfig());
        }
    }
}