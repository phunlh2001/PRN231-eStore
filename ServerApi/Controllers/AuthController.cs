using BusinessObjects.DTOs;
using BusinessObjects.Objects;
using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using ServerApi.Controllers.Base;

namespace ServerApi.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IMemberRepository _repo;
        public AuthController(IMemberRepository repo) => _repo = repo;

        [HttpPost("register")]
        public IActionResult Register([FromBody] Member member)
        {
            if (member == null) return BadRequest("Information invalid");
            if (_repo.CheckEmail(member.Email)) return BadRequest("Email already exists");

            _repo.InsertMember(member);
            return NoContent();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginInfo info)
        {
            if (info == null) return BadRequest("Information is required.");

            if (_repo.Login(info.Email, info.Password))
            {
                var user = _repo.GetMemberByEmail(info.Email);
                return Ok(user);
            }
            else
            {
                return NotFound("Invalid email or password.");
            }
        }
    }
}
