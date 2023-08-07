using Microsoft.AspNetCore.Identity;
using PocketIS.Application.Common.Codes;
using PocketIS.Application.Common.Types;
using PocketIS.Domain;
using PocketIS.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace PocketIS.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IClaimsProviderService _claimsProviderService;

        public IdentityService(IUserService userService,
            IPasswordHasher<User> passwordHasher,
            IJwtService jwtService,
            IRefreshTokenService refreshTokenService,
            IClaimsProviderService claimsProviderService)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _refreshTokenService = refreshTokenService;
            _claimsProviderService = claimsProviderService;
        }

        public async Task SignUpAsync(Guid id, string email, string password, string firstName, string lastName, string role = Role.User)
        {
            var user = await _userService.GetAsync(email);
            if (user != null)
            {
                throw new PocketISException(IdentityCodes.EmailInUse,
                    $"Email: '{email}' is already in use.");
            }
            if (string.IsNullOrWhiteSpace(role))
            {
                role = Role.User;
            }
            user = new User(id, email, role, firstName, lastName);
            user.SetPassword(password, _passwordHasher);
            await _userService.AddAsync(user);
        }

        public async Task<JsonWebToken> SignInAsync(string email, string password)
        {
            var user = await _userService.GetAsync(email);
            if (user == null || !user.ValidatePassword(password, _passwordHasher))
            {
                throw new PocketISException(IdentityCodes.InvalidCredentials,
                    "Invalid credentials.");
            }
            var refreshToken = new RefreshToken(user, _passwordHasher);
            var claims = await _claimsProviderService.GetAsync(user.Id);
            var jwt = _jwtService.CreateToken(user.Id.ToString("N"), user.Role, claims);
            jwt.RefreshToken = refreshToken.Token;
            await _refreshTokenService.AddAsync(refreshToken);

            return jwt;
        }

        public async Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            var user = await _userService.GetAsync(userId);
            if (user == null)
            {
                throw new PocketISException(IdentityCodes.UserNotFound,
                    $"User with id: '{userId}' was not found.");
            }
            if (!user.ValidatePassword(currentPassword, _passwordHasher))
            {
                throw new PocketISException(IdentityCodes.InvalidCurrentPassword,
                    "Invalid current password.");
            }
            user.SetPassword(newPassword, _passwordHasher);
            await _userService.UpdateAsync(user);
        }

        public async Task<User> GetUserByIdAsync(Guid id)
            => await _userService.GetAsync(id);
    }
}
