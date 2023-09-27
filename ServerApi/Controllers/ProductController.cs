using BusinessObjects.Objects;
using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ServerApi.Controllers.Base;
using System.Linq;

namespace ServerApi.Controllers
{
    [EnableCors("AllowAllOrigins")]
    public class ProductController : BaseController
    {
        private readonly IProductRepository _repo;
        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_repo.GetList());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var prod = _repo.GetProduct(id);
            if (prod == null) return NotFound("Product not found.");

            return Ok(prod);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Product prod)
        {
            if (prod == null) return BadRequest("Information is required");

            if (ModelState.IsValid) _repo.InsertProduct(prod);
            else return BadRequest("Invalid");

            return NoContent();
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] Product prod)
        {
            if (prod == null) return BadRequest("information is required");

            var p = _repo.GetProduct(id);
            if (p == null) return NotFound("Product not found");

            _repo.UpdateProduct(prod);
            return Ok(p);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var p = _repo.GetProduct(id);
            if (p == null) return NotFound("Product not found");

            _repo.DeleteProduct(id);
            return NoContent();
        }

        [HttpGet("search")]
        public IActionResult GetAllByName([FromQuery] string text)
        {
            var listByName = _repo.GetAllByName(text);
            if (listByName.Count() == 0)
            {
                return NotFound("Product name not found.");
            }
            return Ok(listByName);

        }

        [HttpGet("{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            var product = _repo.GetByName(name);
            if (product == null)
            {
                return NotFound("Product name not found.");
            }
            return Ok(product);

        }
    }
}
