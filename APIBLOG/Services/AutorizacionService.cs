using APIBLOG.Models;
using APIBLOG.Models.Custom;
using APIBLOG.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace APIBLOG.Services
{

    public class AutorizacionService : IAutorizacionService
    {
        private readonly ApiblogContext _context;
        private readonly IConfiguration _configuration;

        public AutorizacionService(ApiblogContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private string GenerarToken(string idUsuario,string rol) 
        {
            var key = _configuration.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUsuario));
            claims.AddClaim(new Claim(ClaimTypes.Role, rol));

            var credencialesToken = new SigningCredentials(
                   new SymmetricSecurityKey(keyBytes),
                   SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = credencialesToken

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokenCreado = tokenHandler.WriteToken(tokenConfig);

            return tokenCreado;

        }
        public async Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion)
        {
            var usuario_encontrado = _context.Usuarios
                .Include(u=>u.IdRolNavigation)
                .FirstOrDefault(x=>
            x.NombreUsuario == autorizacion.Usuario && 
            x.Contraseña == autorizacion.Password);

            if (usuario_encontrado==null)
            {
                return await Task.FromResult<AutorizacionResponse>(null);
            }

            string rol = usuario_encontrado.IdRolNavigation.Nombre;
            string tokenCreado = GenerarToken(usuario_encontrado.IdUsuario.ToString(),rol);
            string refreshTokenCreado = GenerarRefreshToken();

            //return new AutorizacionResponse { Token = tokenCreado, Resultado = true, Msg = "Ok" };
            return await GuardarHistorialRefreshToken(usuario_encontrado.IdUsuario,tokenCreado,refreshTokenCreado);

        }

        private string GenerarRefreshToken()
        {
            var byteArray = new byte[64];
            var refreshToken = "";

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(byteArray);
                refreshToken = Convert.ToBase64String(byteArray);
            }
            return refreshToken;
        }


        private async Task<AutorizacionResponse>GuardarHistorialRefreshToken(
            Guid idUsuario,
            string token,
            string refreshToken
            ){

            var historialRefreshToken = new HistorialRefreshToken
            {
                IdUsuario = idUsuario,
                Token = token,
                RefreshToken = refreshToken,
                FechaCreacion = DateTime.UtcNow,
                FechaExpiracion = DateTime.UtcNow.AddMinutes(60)

            };
            await _context.HistorialRefreshTokens.AddAsync(historialRefreshToken);
            await _context.SaveChangesAsync();

            return new AutorizacionResponse { Token = token, RefreshToken = refreshToken, Resultado = true, Msg = "Ok" };
            }


        public async Task<AutorizacionResponse> DevolverRefreshToken(RefreshTokenRequest refreshTokenRequest, Guid idUsuario)
        {
            var refreshTokenEncontrado = _context.HistorialRefreshTokens.FirstOrDefault(x =>
            x.Token == refreshTokenRequest.TokenExpirado &&
            x.RefreshToken == refreshTokenRequest.RefreshToken &&
            x.IdUsuario == idUsuario);

            if (refreshTokenEncontrado == null)
                return new AutorizacionResponse { Resultado = false, Msg = "No existe refreshToken" };

            // Obtener el usuario y su rol
            var usuario = await _context.Usuarios.Include(u => u.IdRolNavigation)
                                                 .FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);

            if (usuario == null)
                return new AutorizacionResponse { Resultado = false, Msg = "Usuario no encontrado" };

            var rol = usuario.IdRolNavigation.Nombre;
            var refreshTokenCreado = GenerarRefreshToken();
            var tokenCreado = GenerarToken(idUsuario.ToString(),rol);

            return await GuardarHistorialRefreshToken(idUsuario, tokenCreado, refreshTokenCreado);
        }
    }
}
