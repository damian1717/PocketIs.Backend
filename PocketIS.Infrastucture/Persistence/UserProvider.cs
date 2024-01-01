using Microsoft.AspNetCore.Http;
using PocketIS.Application.Common.Interfaces;
using PocketIS.Common;

namespace PocketIS.Infrastucture.Persistence
{
    public class UserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid? GetUserId()
            => string.IsNullOrWhiteSpace(_httpContextAccessor.HttpContext?.User?.Identity?.Name)
            ? Guid.Empty :
              Guid.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);

        public Guid? GetCompanyId()
            => string.IsNullOrWhiteSpace(_httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == Constants.CompanyId)?.Value)
            ? Guid.Empty :
              Guid.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == Constants.CompanyId).Value);
    }
}
