using PocketIS.Domain;
using System.Collections.Generic;

namespace PocketIS.Services.Interfaces
{
    public interface IJwtService
    {
        JsonWebToken CreateToken(string userId, string role = null, IDictionary<string, string> claims = null);
    }
}
