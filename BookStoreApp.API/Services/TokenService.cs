using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStoreApp.API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;


namespace BookStoreApp.API.Services
{
    /// <summary>
    /// Servizio per generare un token JWT per un utente autenticato
    /// </summary>
    public class TokenService
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;

        public TokenService(UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Genera un token JWT contenente le informazioni dell'utente e i suoi ruoli
        /// </summary>
        /// <param name="user">Utente per cui generare il token</param>
        /// <returns>Stringa del token JWT</returns>
        public async Task<string> GenerateTokenAsync(ApiUser user)
        {
            // 1. Recupero le impostazioni JWT da configuration
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = jwtSettings.GetValue<string>("Key");
            var issuer = jwtSettings.GetValue<string>("Issuer");
            var audience = jwtSettings.GetValue<string>("Audience");
            var expiryMinutes = jwtSettings.GetValue<int>("Duration");

            // 2. Recupero i ruoli associati all'utente
            var userRoles = await _userManager.GetRolesAsync(user);

            // 3. Definisco i claim base per il token
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim("FirstName", user.FirstName ?? string.Empty),
                new Claim("LastName", user.LastName ?? string.Empty)
            };

            // 4. Aggiungo i claim per ciascun ruolo dell'utente
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            // 5. Creo la chiave crittografica e le credenziali di firma
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            // 6. Costruisco il token JWT
            var tokenDescriptor = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: signingCredentials
            );

            // 7. Genero e restituisco la stringa del token
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
