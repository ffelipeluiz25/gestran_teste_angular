using GestranApi.Context;
using GestranApi.DTOs.Api;
using GestranApi.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace GestranApi.JWT
{
    public class JwtService
    {
        private readonly GestranDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public JwtService(GestranDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<LoginResponse?> Authenticate(LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Login) || string.IsNullOrWhiteSpace(request.Senha))
                return null;

            var userAccont = await _dbContext.Usuario.FirstOrDefaultAsync(x => x.Login.Equals(request.Login));
            if (userAccont is null || !PasswordCrypt.VerificaPassword(request.Senha, userAccont.Senha))
                return null;

            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];
            var validadeTokenMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
            var expiracaoTempoToken = DateTime.UtcNow.AddMinutes(validadeTokenMins);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(JwtRegisteredClaimNames.Name, request.Login) }),
                Expires = expiracaoTempoToken,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);
            return new LoginResponse
            {
                IdUsuarioLogado = userAccont.Id,
                AcessToken = accessToken,
                TipoUsuario = userAccont.IdTipoUsuario.ToString()
            };
        }

    }
}