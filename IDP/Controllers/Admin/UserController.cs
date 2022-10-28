using Application.Interfaces;
using Application.SearchParams;
using Application.ViewModels.User;
using Application.ViewModels.UserRole;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("roles")]
        public IEnumerable<UserRoleResponse> Search()
        {
            return _userService.UserRoles();
        }

        [HttpGet]
        public Task<IEnumerable<UserResponse>> Search([FromQuery] UserQueryParam parametroBusca)
        {
            return _userService.Search(parametroBusca);
        }

        [HttpGet("{id:int}")]
        public Task<UserResponse> GetById([FromRoute] int id)
        {
            return _userService.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> Add([FromBody] UserRequest model)
        {
            var created = await _userService.Add(model);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public Task<UserResponse> Update([FromRoute] int id, [FromBody] UserRequest model)
        {
            return _userService.Update(id, model);
        }

        [HttpDelete("{id:int}")]
        public Task Remover([FromRoute] int id)
        {
            return _userService.Delete(id);
        }
    }
}
