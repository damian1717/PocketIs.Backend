using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace PocketIS.Domain
{
    public class User : IIdentifiable
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public Guid Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string PasswordHash { get; private set; }
        public DateTime InsertedDate { get; private set; }
        public DateTime UpdatedDate { get; private set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;

        protected User()
        {
        }

        public User(Guid id)
        {
            Id = id;
        }

        public User(Guid id, string email, string role, string firstName, string lastName, Guid companyId)
        {
            if (!EmailRegex.IsMatch(email))
            {
                
                throw new Exception($"Invalid email: '{email}'.");
            }

            if (!Domain.Role.IsValid(role))
            {
                throw new Exception($"Invalid role: '{role}'.");
            }
            Id = id;
            Email = email.ToLowerInvariant();
            CompanyId = companyId;
            FirstName = firstName;
            LastName = lastName;
            Role = role.ToLowerInvariant();
            InsertedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }

        public void SetPassword(string password, IPasswordHasher<User> passwordHasher)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password can not be empty.");
            }
            PasswordHash = passwordHasher.HashPassword(this, password);
        }

        public bool ValidatePassword(string password, IPasswordHasher<User> passwordHasher)
            => passwordHasher.VerifyHashedPassword(this, PasswordHash, password) != PasswordVerificationResult.Failed;
    }
}
