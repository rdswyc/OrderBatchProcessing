using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OrderBatchProcessing.Models
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Boutique> Boutiques { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=OrderBatchProcessing.db");
    }

    public class Boutique
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

        public List<Order> Orders { get; } = new List<Order>();
    }

    public class Order
    {
        public int Id { get; set; }
        public int BoutiqueId { get; set; }
        public decimal Value { get; set; }

        public Boutique Boutique { get; set; }
    }
}