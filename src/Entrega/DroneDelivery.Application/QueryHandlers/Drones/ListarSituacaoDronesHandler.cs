using AutoMapper;
using DroneDelivery.Application.Dtos.Drone;
using DroneDelivery.Application.Queries.Drones;
using DroneDelivery.Data.Repositorios.Interfaces;
using DroneDelivery.Domain.Enum;
using DroneDelivery.Domain.Models;
using DroneDelivery.Shared.Domain.Core.Domain;
using DroneDelivery.Shared.Domain.Core.Enums;
using DroneDelivery.Shared.Domain.Core.Validator;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DroneDelivery.Application.QueryHandlers.Drones
{
    public class ListarSituacaoDronesHandler : ValidatorResponse, IRequestHandler<SituacaoDronesQuery, ResponseResult>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListarSituacaoDronesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseResult> Handle(SituacaoDronesQuery request, CancellationToken cancellationToken)
        {
            var drones = await ObterDronesParaViagem();

            foreach (var drone in drones)
                drone.SepararPedidosParaEntrega();

            _response.AddValue(new
            {
                drones = _mapper.Map<IEnumerable<Drone>, IEnumerable<DroneSituacaoDto>>(drones)
            });

            return _response;
        }

        private async Task<IEnumerable<Drone>> ObterDronesParaViagem()
        {
            var dronesProntos = await _unitOfWork.Drones.ObterDronesParaEntregaAsync();

            foreach (var drone in dronesProntos)
            {
                drone.AtualizarStatus(DroneStatus.EmEntrega);
                foreach (var pedido in drone.Pedidos.Where(x => x.Status == PedidoStatus.EmEntrega))
                {
                    var hist = HistoricoPedido.Criar(drone.Id, pedido.Id);
                    drone.CriarHistorico(hist);
                }
            }

            if (dronesProntos.Any())
            {
                await _unitOfWork.Drones.AdicionarHistoricoAsync(dronesProntos.SelectMany(x => x.HistoricoPedidos));
                await _unitOfWork.SaveAsync();
            }

            return dronesProntos;
        }
    }
}
