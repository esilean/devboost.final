using DroneDelivery.PedidoProducer.Dto;
using DroneDelivery.PedidoProducer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DroneDelivery.PedidoProducer.Controllers
{
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoProducerService _pedidoProducer;

        public PedidosController(IPedidoProducerService pedidoProducer)
        {
            _pedidoProducer = pedidoProducer;
        }

        /// <summary>
        /// Criar um pedido
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/pedidos
        ///     {
        ///        "peso": 10000,
        ///        "valor": 999
        ///     }
        ///
        /// </remarks>        
        /// <param name="pedidoDto"></param>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Adicionar([FromBody] CriarPedidoDto pedidoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors));

            await _pedidoProducer.EnviarPedido(pedidoDto);
            return Ok();
        }
    }
}
