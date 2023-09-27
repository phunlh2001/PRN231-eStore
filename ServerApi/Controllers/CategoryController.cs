using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using ServerApi.Controllers.Base;

namespace ServerApi.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryRepository _repo;
        public CategoryController(ICategoryRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var list = _repo.GetAll();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _repo.GetById(id);
            if (category == null) return NotFound("Not found.");

            return Ok(category);
        }
    }
}
