using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PagoEfectivo.Api.Applications.DTO.Request.PromocionDTOs;
using PagoEfectivo.Api.Applications.DTO.Response.PromocionDTOs;
using PagoEfectivo.Api.Applications.Interfaces;
using PagoEfectivo.Api.Domain.Commands.PromocionCommand;
using PagoEfectivo.Api.Infra;

namespace PagoEfectivo.Api.Applications.Services
{
    public class PromocionAppService : IPromocionAppService
    {
        private readonly IMediator _mediator;
        private readonly PagoEfectivoContext _context;
        public PromocionAppService(IMediator mediator, PagoEfectivoContext context)
        {
            _mediator = mediator;
            _context = context;
        }
        public Task<ValidationResult> Canjear(CanjearPromocionRequestDTO model)
        {
            var promocionCommand = new CanjearPromocionCommand(model.Codigo);
            return _mediator.Send(promocionCommand);
        }
        public async Task<(ValidationResult, CrearPromocionResponseDTO)> Crear(CrearPromocionRequestDTO model)
        {
            var promocionCommand = new CrearPromocionCommand(model.Nombre, model.Email);
            var validationResult = await _mediator.Send(promocionCommand);
            return (validationResult, promocionCommand.Adapt<CrearPromocionResponseDTO>());
        }
        public async Task<IEnumerable<PromocionResponseDTO>> Listar()
        {
            /* https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.entityframeworkqueryableextensions.asnotrackingwithidentityresolution?view=efcore-5.0 
            https://www.youtube.com/watch?v=Wl5NX5tkh6w
            */

            var response = await _context.Promocion
                    .Include(x => x.Estado)
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync();

            return response.Adapt<IEnumerable<PromocionResponseDTO>>();
        }
    }
}