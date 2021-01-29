using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OrderBatchProcessing.Models
{
    public class BoutiqueBindingModel
    {
        public string BoutiqueId { get; set; }

        [Required(ErrorMessage = "{0} cannot be empty.")]
        public string Name { get; set; }

        public string City { get; set; }

        public List<OrderViewModel> Orders { get; set; }
    }

    public static class BoutiqueExtension
    {
        public static BoutiqueBindingModel ToViewModel(this Boutique b, bool includeOrders = false) => new BoutiqueBindingModel
        {
            BoutiqueId = $"B{b.Id}",
            Name = b.Name,
            City = b.City,
            Orders = includeOrders ? b.Orders.Select(o => (OrderViewModel)o).ToList() : null
        };
    }
}
