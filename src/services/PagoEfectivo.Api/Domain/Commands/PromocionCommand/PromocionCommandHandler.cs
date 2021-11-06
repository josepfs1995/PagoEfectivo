using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Mapster;
using MediatR;
using PagoEfectivo.Api.Domain.Commands.PromocionCommand;
using PagoEfectivo.Api.Domain.Const;
using PagoEfectivo.Api.Domain.Model;
using PagoEfectivo.Api.Infra;

namespace PagoEfectivo.Api.Commands.PromocionCommand
{
    public class PromocionCommandHandler : IRequestHandler<CrearPromocionCommand, ValidationResult>,
                                        IRequestHandler<CanjearPromocionCommand, ValidationResult>
    {
        /*No se implemento patron repositorio porque utilizare el que viene con DbSet,
        No se implemento UnitOfWork porque implementare el de DbContext*/
        private readonly PagoEfectivoContext _context;
        public PromocionCommandHandler(PagoEfectivoContext context)
        {
            _context = context;
        }
        public async Task<ValidationResult> Handle(CrearPromocionCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var promocion = _context.Promocion
                        .Where(x => x.Email == request.Email &&
                                x.Id_Estado == EstadoConst.GENERADO)
                        .FirstOrDefault();

            if (promocion != null)
            {
                request.ValidationResult.Errors.Add(new ValidationFailure(nameof(request.Email), $"Existe una promoción activa con el email: {request.Email}"));
                return request.ValidationResult;
            }
            request.Codigo = GeneraryValidarCodigo();

            promocion = request.Adapt<Promocion>();
            _context.Promocion.Add(promocion);
            await _context.SaveChangesAsync();

            return request.ValidationResult;
        }
        public async Task<ValidationResult> Handle(CanjearPromocionCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var promocion = _context.Promocion
                       .Where(x => x.Codigo == request.Codigo &&
                                    x.Id_Estado == EstadoConst.GENERADO)
                       .FirstOrDefault();

            if (promocion == null)
            {
                request.ValidationResult.Errors.Add(new ValidationFailure(nameof(request.Codigo), $"El cupon {request.Codigo} no existe."));
                return request.ValidationResult;
            }
            promocion.Id_Estado = EstadoConst.CANJEADO;
            _context.Promocion.Update(promocion);
            await _context.SaveChangesAsync();

            return request.ValidationResult;
        }

        private string GenerarCodigo()
        {
            var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(Enumerable.Repeat(alphabet, 6)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
        private string GeneraryValidarCodigo(int attempt = 0)
        {
            /*Se puso como maximo generar 5 veces el código*/
            if (attempt == 5) throw new Exception("Ocurrío un error");

            var codigo = GenerarCodigo();
            var promocion = _context.Promocion.FirstOrDefault(x => x.Codigo == codigo);
            if (promocion != null)
                return  GeneraryValidarCodigo(attempt++);
            return codigo;
        }
    }
}