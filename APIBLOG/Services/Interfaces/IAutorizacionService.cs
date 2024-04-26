using APIBLOG.Models.Custom;

namespace APIBLOG.Services.Interfaces
{
    public interface IAutorizacionService
    {
        Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion);

        Task<AutorizacionResponse> DevolverRefreshToken(RefreshTokenRequest refreshTokenRequest,Guid idUsuario);
    }
}
