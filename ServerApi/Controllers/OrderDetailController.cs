using BusinessObjects.Objects;
using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using ServerApi.Controllers.Base;

namespace ServerApi.Controllers
{
    public class OrderDetailController : BaseController
    {
        private readonly IOrderDetailRepository _repo;
        public OrderDetailController(IOrderDetailRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var list = _repo.GetList();
            if (list == null) return NotFound("Order detail empty.");

            return Ok(list);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] OrderDetail detail)
        {
            if (detail == null) return BadRequest("Information is required.");

            if (ModelState.IsValid)
            {
                _repo.Add(detail);
                return NoContent();
            }
            else
            {
                return BadRequest("Invalid");
            }

        }
    }
}
