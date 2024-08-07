﻿using Microsoft.AspNetCore.Identity;
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
        private readonly ICompanyService _companyService;
        public IdentityService(IUserService userService,
            IPasswordHasher<User> passwordHasher,
            IJwtService jwtService,
            IRefreshTokenService refreshTokenService,
            IClaimsProviderService claimsProviderService,
            ICompanyService companyService)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _refreshTokenService = refreshTokenService;
            _claimsProviderService = claimsProviderService;
            _companyService = companyService;
        }

        public async Task SignUpAsync(Guid id, string email, string password, string firstName, string lastName, string companyCode, Guid companyId, string role = Role.User)
        {
            var user = await _userService.GetAsync(email);
            if (user != null)
            {
                throw new PocketISException(IdentityCodes.EmailInUse,
                    $"Email: '{email}' jest już uzywany.");
            }

            var correctCompanyId = await SetCompanyId(companyCode, companyId);

            if (string.IsNullOrWhiteSpace(role))
            {
                role = Role.User;
            }
            var newUser = new User(id, email, role, firstName, lastName, correctCompanyId);
            newUser.SetPassword(password, _passwordHasher);
            await _userService.AddAsync(newUser);
        }

        public async Task<JsonWebToken> SignInAsync(string email, string password)
        {
            var user = await _userService.GetUserForIdentityAsync(email);
            if (user == null || !user.ValidatePassword(password, _passwordHasher))
            {
                throw new PocketISException(IdentityCodes.InvalidCredentials,
                    "Niewłaściwe dane.");
            }

            var refreshToken = new RefreshToken(user, _passwordHasher);
            var claims = await _claimsProviderService.GetAsync(user.Id, user.CompanyId);
            var jwt = _jwtService.CreateToken(user.Id.ToString("N"), user.Role, claims);
            jwt.RefreshToken = refreshToken.Token;
            await _refreshTokenService.AddAsync(refreshToken);

            return jwt;
        }

        public async Task ChangePasswordAsync(string email, string currentPassword, string newPassword)
        {
            var user = await _userService.GetUserForIdentityAsync(email);
            if (user == null)
            {
                throw new PocketISException(IdentityCodes.UserNotFound,
                    $"Użytkownik z email: '{email}' nie został znaleziony.");
            }
            if (!user.ValidatePassword(currentPassword, _passwordHasher))
            {
                throw new PocketISException(IdentityCodes.InvalidCurrentPassword,
                    "Niewłaściwe dane.");
            }
            user.SetPassword(newPassword, _passwordHasher);
            await _userService.UpdateAsync(user);
        }

        private async Task<Guid> SetCompanyId(string companyCode, Guid companyId)
        {
            if (companyId != Guid.Empty)
            {
                return companyId;
            }
            else
            {
                var company = await _companyService.GetCompanyByCodeAsync(companyCode);
                if (company is null)
                {
                    throw new PocketISException(CompanyCodes.CompanyNotExist,
                        $"Code firmy: '{companyCode}' nie istnieje.");
                }

                return company.Id;
            }
        }
    }
}
