using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocketIS.Domain;
using PocketIS.Models.Users;
using PocketIS.Services.Interfaces;

namespace PocketIS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult<User>> GetUserById([FromRoute] Guid id)
            => Ok(await _userService.GetAsync(id));

        [HttpGet("getuserbyemail/{email}")]
        public async Task<ActionResult<User>> GetUserByEmail([FromRoute] string email)
            => Ok(await _userService.GetAsync(email));

        [HttpGet("GetUsers")]
        public async Task<ActionResult<User>> GetUsers()
            => Ok(await _userService.GetAllUsersAsync(CompanyId, IsSuperAdmin));

        [HttpPost]
        [Route("updateuser")]
        public async Task<IActionResult> UpdateUser(UpdateUserModel model)
        {
            if (model?.Id is null) return BadRequest();

            var user = new User(model.Id, model.Email, model.Role, model.FirstName, model.LastName, Guid.Empty);

            await _userService.UpdateAsync(user);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteAsync(id);
            return Ok();
        }
    }
}
