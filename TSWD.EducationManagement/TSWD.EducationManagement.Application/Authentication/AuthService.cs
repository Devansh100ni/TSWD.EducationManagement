using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TSWD.EducationManagement.Domain.DTOs.Auth;
using TSWD.EducationManagement.Domain.Entities;
using TSWD.EducationManagement.EntityFrameworkCore;
using TSWD.EducationManagement.EntityFrameworkCore.Infrastructure;
using TSWD.EducationManagement.Shared.Common;
using TSWD.EducationManagement.Shared.Helpers;

namespace TSWD.EducationManagement.Application.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<AppUser> repository;
        private readonly EducationDbContext dbContext;
        private readonly IConfiguration configuration;
        private readonly IRepository<AppRole> roleRepository;

        public AuthService(IRepository<AppUser> repository,
                            EducationDbContext dbContext,
                            IConfiguration configuration,
                            IRepository<AppRole> roleRepository)
        {
            this.repository = repository;
            this.dbContext = dbContext;
            this.configuration = configuration;
            this.roleRepository = roleRepository;
        }

        public async Task<Result<string>> LoginAsync(LoginRequest request)
        {
            AppUser? user = new AppUser();
            if (!string.IsNullOrEmpty(request.TenantName))
            {
                var tanent = dbContext.AppTenants.FirstOrDefault(t => t.Name.ToLower().Equals(request.TenantName.ToLower()));
                user = dbContext.AppUser.FirstOrDefault(u => u.Email == request.Email && u.TenantId == tanent.Id);
            }
            else
                user = dbContext.AppUser.FirstOrDefault(u => u.Email == request.Email && u.TenantId == null);

            if (user == null || string.IsNullOrEmpty(user.PasswordHash) ||
                !Encrypt.VerifyPassword(request.Password, user.PasswordHash))
            {
                return Result<string>.Fail("Invalid email or password.");
            }

            var role = await roleRepository.GetByIdAsync(user.RoleId.Value);

            var token = GenerateToken(user.Email, user.UserName, role.Name, user.Id, Convert.ToString(user.TenantId));

            return Result<string>.Ok(token, "Login successful");
        }

        private string GenerateToken(string email, string userName, string role, Guid userId, string? tenantId = null)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.UserData, tenantId)
            };

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"], claims, expires: DateTime.Now.AddDays(1), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
