using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OrderBatchProcessing.Models;
using OrderBatchProcessing.Repositories;

namespace OrderBatchProcessing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComissionsController : Controller
    {
        private readonly BoutiqueRepository repository;

        public ComissionsController(BoutiqueRepository repos)
        {
            repository = repos;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            var boutiques = repository.GetList(true);
            var query = boutiques.GroupBy(
                b => b.Id,
                (boutiqueId, boutiques) => new ComissionViewModel
                {
                    BoutiqueId = $"B{boutiqueId}",
                    TotalComission = ComissionValue(boutiqueId, boutiques)
                }
            );

            return Ok(query);
        }

        private decimal ComissionValue(int boutiqueId, IEnumerable<Boutique> boutiques)
        {
            var orders = boutiques.SelectMany(b => b.Orders);

            if (orders.Count() > 1)
            {
                var maxOrder = orders.Max(o => o.Value);
                orders = orders.Where(o => o.Value != maxOrder);
            }

            var comission = orders.Sum(o => o.Value) / 10;

            Console.WriteLine($"B{boutiqueId},{comission}");

            return comission;
        }
    }
}
