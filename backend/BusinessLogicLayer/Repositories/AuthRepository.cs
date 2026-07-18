using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Classes;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogicLayer.Repositories
{
    public class AuthRepository(
        IUserRepository userRepository,
        IAdminRepository adminRepository,
        IOwnerRepository ownerRepository,
        ITenantRepository tenantRepository,
        IConfiguration config) : IAuthRepository
    {
        public (string AccessToken, string RefreshToken, int ExpiresIn) RegisterAdmin(Admin admin, string plainPassword)
        {
            EnsureEmailAvailable(admin.Email);

            admin.Password = BCrypt.Net.BCrypt.HashPassword(plainPassword);
            adminRepository.Add(admin);
            adminRepository.Save();

            return GenerateTokens(admin.UsserId, admin.Email, "Admin");
        }

        public (string AccessToken, string RefreshToken, int ExpiresIn) RegisterOwner(Owner owner, string plainPassword)
        {
            EnsureEmailAvailable(owner.Email);

            owner.Password = BCrypt.Net.BCrypt.HashPassword(plainPassword);
            ownerRepository.Add(owner);
            ownerRepository.Save();

            return GenerateTokens(owner.UsserId, owner.Email, "Owner");
        }

        public (string AccessToken, string RefreshToken, int ExpiresIn) RegisterTenant(Tentant tenant, string plainPassword)
        {
            EnsureEmailAvailable(tenant.Email);

            tenant.Password = BCrypt.Net.BCrypt.HashPassword(plainPassword);
            tenantRepository.Add(tenant);
            tenantRepository.Save();

            return GenerateTokens(tenant.UsserId, tenant.Email, "Tenant");
        }

        public (string AccessToken, string RefreshToken, int ExpiresIn, string Role) Login(string email, string password)
        {
            var user = userRepository.GetByEmail(email);

            if (user is null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                throw new UnauthorizedAccessException("Invalid credentials");

            var role = user switch
            {
                Admin => "Admin",
                Owner => "Owner",
                Tentant => "Tenant",
                _ => throw new InvalidOperationException("Unknown user type")
            };

            var (accessToken, refreshToken, expiresIn) = GenerateTokens(user.UsserId, user.Email, role);
            return (accessToken, refreshToken, expiresIn, role);
        }

        private void EnsureEmailAvailable(string email)
        {
            if (userRepository.GetByEmail(email) is not null)
                throw new InvalidOperationException("Email already in use");
        }

        private (string AccessToken, string RefreshToken, int ExpiresIn) GenerateTokens(int userId, string email, string role)
        {
            var accessSecret = config["Jwt:AccessSecret"]
                ?? throw new InvalidOperationException("Jwt:AccessSecret is not set");
            var issuer = config["Jwt:Issuer"];
            var audience = config["Jwt:Audience"];
            var accessMinutes = int.Parse(config["Jwt:AccessTokenMinutes"] ?? "15");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accessSecret));
            var accessToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(accessMinutes),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            var accessTokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);
            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            return (accessTokenString, refreshToken, accessMinutes * 60);
        }
    }
}