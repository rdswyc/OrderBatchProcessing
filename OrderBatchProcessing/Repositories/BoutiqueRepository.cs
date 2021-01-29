using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OrderBatchProcessing.Models;

namespace OrderBatchProcessing.Repositories
{
    public class BoutiqueRepository : IDisposable
    {
        private readonly DataBaseContext context;

        public BoutiqueRepository(DataBaseContext ctx)
        {
            context = ctx;
        }

        public Boutique Add(Boutique boutique)
        {
            var created = context.Boutiques.Add(boutique);
            context.SaveChanges();

            AddRandomOrders(created.Entity.Id);
            return created.Entity;
        }

        public Boutique Delete(int id)
        {
            var boutique = context.Boutiques.Find(id);
            context.Boutiques.Remove(boutique);
            context.SaveChanges();

            return boutique;
        }

        public bool Exists(int id)
        {
            return context.Boutiques.Any(e => e.Id == id);
        }

        public List<Boutique> GetList(bool includeOrders = false)
        {
            return includeOrders ?
                context.Boutiques.Include(b => b.Orders).ToList() :
                context.Boutiques.ToList();
        }

        public Boutique GetSingle(int id)
        {
            var boutique = context.Boutiques
                .Include(b => b.Orders)
                .FirstOrDefault(b => b.Id == id);

            return boutique;
        }

        public void Update(Boutique boutique)
        {
            context.Entry(boutique).State = EntityState.Modified;

            context.SaveChanges();
        }

        private void AddRandomOrders(int boutiqueId)
        {
            var rng = new Random();
            var orders = Enumerable.Range(1, rng.Next(5)).Select(index => new Order
            {
                BoutiqueId = boutiqueId,
                Value = rng.Next(500)
            });

            context.Orders.AddRange(orders);
            context.SaveChanges();
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
