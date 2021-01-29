using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OrderBatchProcessing.Models;
using OrderBatchProcessing.Repositories;

namespace OrderBatchProcessing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoutiqueController : Controller
    {
        private readonly BoutiqueRepository repository;

        public BoutiqueController(BoutiqueRepository repos)
        {
            repository = repos;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            var items = repository.GetList();

            var model = items.Select(b => b.ToViewModel());
            return Ok(model);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get([FromRoute] int id)
        {
            var item = repository.GetSingle(id);

            if (item == null)
            {
                return NotFound(new { message = "Boutique not found." });
            }

            var model = item.ToViewModel(true);
            return Ok(model);
        }

        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody] Boutique item)
        {
            Boutique created;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                created = repository.Add(item);
            }
            catch
            {
                return BadRequest(new { message = "Error trying to insert Boutique." });
            }

            var model = item.ToViewModel(true);
            return CreatedAtAction("Get", new { id = created.Id }, model);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Boutique item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.Id)
            {
                return BadRequest(new { message = "Check the Boutique to update." });
            }
            if (!repository.Exists(id))
            {
                return NotFound(new { message = "Boutique not found." });
            }

            try
            {
                repository.Update(item);
            }
            catch
            {
                return BadRequest(new { message = "Error trying to update Boutique." });
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (!repository.Exists(id))
            {
                return NotFound(new { message = "Boutique not found." });
            }

            Boutique deleted;

            try
            {
                deleted = repository.Delete(id);
            }
            catch
            {
                return BadRequest(new { message = "Error trying to delete Boutique." });
            }

            var model = deleted.ToViewModel(true);
            return Ok(model);
        }
    }
}
