using Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Presentation;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service {

    public interface IAuthService {
        Task<string> Login(string username, string password);
    }


    public class AuthService : IAuthService {

        private readonly ISystemAccountRepo _accountRepo;
        private readonly IConfiguration _config;

        public AuthService(ISystemAccountRepo accountRepo, IConfiguration config) {
            _accountRepo = accountRepo;
            _config = config;
        }

        public async Task<string> Login(string username, string password) {
            SystemAccount account =  FindAdminInJson(username, password);
            
            account = account == null ? await _accountRepo.FindByEmailPassword(username, password) : account;

            return GenerateJwtToken(account);
        }

        private SystemAccount FindAdminInJson(string username, string password) {

            var adminName = _config.GetSection("Admin:Username").Value;
            var adminPwd = _config.GetSection("Admin:Password").Value;

            if (adminName == username && adminPwd == password) {
                SystemAccount account = new SystemAccount();
                account.AccountEmail = adminName;
                account.AccountRole = 99;
                return account;
            }
            return null;
        }

        private string GenerateJwtToken(SystemAccount account) {
            if (account == null) {
                return null;
            }
            var key = _config.GetSection("JWTSection:SecretKey").Value;
            var claims = new[] {
                new Claim(ClaimTypes.Email, account.AccountEmail),
                new Claim(ClaimTypes.Role, account.AccountRole.ToString()),
            };

            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
