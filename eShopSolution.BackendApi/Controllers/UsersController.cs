using eShopSolution.Application.Systems.Users;
using eShopSolution.Data.Migrations;
using eShopSolution.ViewModels.Systems.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // /api/user/authenticate
        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var resultTolen = await _userService.Authencate(request);
            if (string.IsNullOrEmpty(resultTolen))
                return BadRequest("UserName or Password is incorrect.");

            return Ok(resultTolen);
        }

        // /api/user/register
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _userService.Register(request);
            if (result)
                return Ok();
            return BadRequest();
        }

        // /api/user/paging?pageIndex=1&pageSize=1&keyword=
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]GetUserPagedingRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var users = await _userService.GetUserPaging(request);
            return Ok(users);
        }
    }
}