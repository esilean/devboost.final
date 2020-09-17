using System;
using System.ComponentModel.DataAnnotations;

namespace DroneDelivery.PedidoProducer.Dto
{
    public class CriarPedidoDto
    {

        public Guid Id { get; }

        [Required]
        [Range(1, 12000)]
        public double Peso { get; set; }

        [Required]
        public double Valor { get; set; }

        public CriarPedidoDto()
        {
            Id = Guid.NewGuid();
        }
    }
}
