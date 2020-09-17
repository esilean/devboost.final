using DroneDelivery.Domain.Models;
using DroneDelivery.Shared.Utility.Dtos;

namespace DroneDelivery.Application.Interfaces
{
    public interface IGeradorToken
    {
        JsonWebTokenDto GerarToken(Usuario usuario);

        RefreshTokenDto GerarRefreshToken(Usuario usuario);
    }
}
