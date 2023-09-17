using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocketIS.Domain;
using PocketIS.Infrastucture.Attributes;
using PocketIS.Models.Identity;
using PocketIS.Services.Interfaces;

namespace PocketIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : BaseController
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [Authorize]
        [HttpGet("me")]
        [JwtAuth]
        public IActionResult Get() => Content($"Your id: '{UserId:N}'.");

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUpRequest request)
        {
            await _identityService.SignUpAsync(request.Id,
                request.Email, request.Password, request.FirstName, request.LastName, request.CompanyCode, CompanyId, request.Role);

            return NoContent();
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(SignInRequest request)
            => Ok(await _identityService.SignInAsync(request.Email, request.Password));

        [HttpPut("me/password")]
        [JwtAuth]
        public async Task<ActionResult> ChangePassword(ChangePasswordRequest request)
        {
            await _identityService.ChangePasswordAsync(request.Email,
                request.CurrentPassword, request.NewPassword);

            return NoContent();
        }
    }
}
