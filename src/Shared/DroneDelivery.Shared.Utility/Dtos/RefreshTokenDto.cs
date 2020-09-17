using System;

namespace DroneDelivery.Shared.Utility.Dtos
{
    public class RefreshTokenDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime DataExpiracao { get; set; }
    }
}
