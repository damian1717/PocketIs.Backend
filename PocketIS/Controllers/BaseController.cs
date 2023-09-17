using Microsoft.AspNetCore.Mvc;
using PocketIS.Common;

namespace PocketIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected bool IsAdmin
            => User.IsInRole("admin");

        protected Guid UserId
            => string.IsNullOrWhiteSpace(User?.Identity?.Name) ?
                Guid.Empty :
                Guid.Parse(User.Identity.Name);

        protected Guid CompanyId
        {
            get 
            { 
                var value = User.Claims.FirstOrDefault(x => x.Type == Constants.CompanyId)?.Value;

                if (Guid.TryParse(value, out Guid companyId))
                {
                    return companyId;
                }
                return Guid.Empty;
            }
        }
    }
}
