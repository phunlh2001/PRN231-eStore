using BusinessObjects.Objects;
using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using ServerApi.Controllers.Base;

namespace ServerApi.Controllers
{
    public class UserController : BaseController
    {
        private readonly IMemberRepository _repo;
        public UserController(IMemberRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var list = _repo.GetList();
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var m = _repo.GetMemberById(id);
            if (m == null) return NotFound("User not found.");

            return Ok(m);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] Member member)
        {
            if (member == null) return BadRequest("Information is required");

            var user = _repo.GetMemberById(id);
            if (user == null) return NotFound("User not found.");

            _repo.UpdateMember(member);
            return NoContent();
        }
    }
}
